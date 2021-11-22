//using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;

namespace bugeto_stor.common
{
   public class passwordhasher
    {
        private readonly bool _useaspnetcore;
        private readonly byte _formatmarker;
        
        private readonly HashAlgorithmName _hashalgorithmname;

        private readonly bool _includeheaderinfo;
        private readonly int _saltLength;
        private readonly int _requestedlength;
        private readonly int _itercount;
        private uint _prf;

        public passwordhasher()
        {
            _useaspnetcore = true;
            _formatmarker = 0x01;
            //_prf = KeyDerivationPrf.HMACSHA256; // Requires Microsoft.AspNetCore
            _hashalgorithmname = HashAlgorithmName.SHA256;
            _includeheaderinfo = true;
            _saltLength = 128 / 8; // bits/1 byte = 16
            _requestedlength = 256 / 8; // bits/1 byte = 32        
            _itercount = 10000;
        }

        public string HashPassword(string password)
        {
            if (string.IsNullOrEmpty(password)) throw new ArgumentNullException(nameof(password));

            byte[] salt = new byte[_saltLength];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            byte[] subkey = new byte[_requestedlength];
            if (_useaspnetcore)
            {
                subkey = KeyDerivation.Pbkdf2(password, salt, _prf, _itercount, _requestedlength);
            }
            else
            {
                using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, _itercount, _hashalgorithmname);
                subkey = pbkdf2.GetBytes(_requestedlength);
            }

            var headerByteLength = 1; // Format marker only
            if (_includeheaderinfo) headerByteLength = 13;

            var outputBytes = new byte[headerByteLength + salt.Length + subkey.Length];

            outputBytes[0] = (byte)_formatmarker;

            if (_includeheaderinfo)
            {
                if (_useaspnetcore)
                {
                    WriteNetworkByteOrder(outputBytes, 1, (uint)_prf);
                }
                else
                {
                    var shaInt = 1;
                    if (_hashalgorithmname == HashAlgorithmName.SHA1) shaInt = 0;
                    else if (_hashalgorithmname == HashAlgorithmName.SHA256) shaInt = 1;
                    else if (_hashalgorithmname == HashAlgorithmName.SHA512) shaInt = 2;

                    WriteNetworkByteOrder(outputBytes, 1, (uint)shaInt);
                }

                WriteNetworkByteOrder(outputBytes, 5, (uint)_itercount);
                WriteNetworkByteOrder(outputBytes, 9, (uint)_saltLength);
            }

            Buffer.BlockCopy(salt, 0, outputBytes, headerByteLength, salt.Length);
            Buffer.BlockCopy(subkey, 0, outputBytes, headerByteLength + _saltLength, subkey.Length);

            return Convert.ToBase64String(outputBytes);
        }

        public bool VerifyPassword(string hashedPassword, string enteredPassword)
        {
            if (string.IsNullOrEmpty(enteredPassword) || string.IsNullOrEmpty(hashedPassword)) return false;

            byte[] decodedHashedPassword;
            try
            {
                decodedHashedPassword = Convert.FromBase64String(hashedPassword);
            }
            catch (Exception)
            {
                return false;
            }

            if (decodedHashedPassword.Length == 0) return false;

            // Read the format marker          
            var verifyMarker = (byte)decodedHashedPassword[0];
            if (_formatmarker != verifyMarker) return false;

            try
            {
                if (_includeheaderinfo)
                {
                    // Read header information
                    var shaUInt = (uint)ReadNetworkByteOrder(decodedHashedPassword, 1);
                    var verifyPrf = shaUInt switch
                    {
                        0 => KeyDerivationPrf.HMACSHA1,
                        1 => KeyDerivationPrf.HMACSHA256,
                        2 => KeyDerivationPrf.HMACSHA512,
                        _ => KeyDerivationPrf.HMACSHA256,
                    };
                    //if (_prf != verifyPrf) return false;

                    var verifyAlgorithmName = shaUInt switch
                    {
                        0 => HashAlgorithmName.SHA1,
                        1 => HashAlgorithmName.SHA256,
                        2 => HashAlgorithmName.SHA512,
                        _ => HashAlgorithmName.SHA256,
                    };
                    if (_hashalgorithmname != verifyAlgorithmName) return false;

                    int iterCountRead = (int)ReadNetworkByteOrder(decodedHashedPassword, 5);
                    if (_itercount != iterCountRead) return false;

                    int saltLengthRead = (int)ReadNetworkByteOrder(decodedHashedPassword, 9);
                    if (_saltLength != saltLengthRead) return false;
                }

                var headerByteLength = 1; // Format marker only
                if (_includeheaderinfo) headerByteLength = 13;

                // Read the salt
                byte[] salt = new byte[_saltLength];
                Buffer.BlockCopy(decodedHashedPassword, headerByteLength, salt, 0, salt.Length);

                // Read the subkey (the rest of the payload)
                int subkeyLength = decodedHashedPassword.Length - headerByteLength - salt.Length;

                if (_requestedlength != subkeyLength) return false;

                byte[] expectedSubkey = new byte[subkeyLength];
                Buffer.BlockCopy(decodedHashedPassword, headerByteLength + salt.Length, expectedSubkey, 0, expectedSubkey.Length);

                // Hash the incoming password and verify it
                byte[] actualSubkey = new byte[_requestedlength];
                if (_useaspnetcore)
                {
                    actualSubkey = KeyDerivation.Pbkdf2(enteredPassword, salt, _prf, _itercount, subkeyLength);
                }
                else
                {
                    using var pbkdf2 = new Rfc2898DeriveBytes(enteredPassword, salt, _itercount, _hashalgorithmname);
                    actualSubkey = pbkdf2.GetBytes(_requestedlength);
                }

                return ByteArraysEqual(actualSubkey, expectedSubkey);
            }
            catch
            {
                // This should never occur except in the case of a malformed payload, where
                // we might go off the end of the array. Regardless, a malformed payload
                // implies verification failed.
                return false;
            }
        }

        // Compares two byte arrays for equality. The method is specifically written so that the loop is not optimized.
        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
        private static bool ByteArraysEqual(byte[] a, byte[] b)
        {
            if (a == null && b == null) return true;
            if (a == null || b == null || a.Length != b.Length) return false;
            var areSame = true;
            for (var i = 0; i < a.Length; i++) { areSame &= (a[i] == b[i]); }
            return areSame;
        }

        private static uint ReadNetworkByteOrder(byte[] buffer, int offset)
        {
            return ((uint)(buffer[offset + 0]) << 24)
                | ((uint)(buffer[offset + 1]) << 16)
                | ((uint)(buffer[offset + 2]) << 8)
                | ((uint)(buffer[offset + 3]));
        }

        private static void WriteNetworkByteOrder(byte[] buffer, int offset, uint value)
        {
            buffer[offset + 0] = (byte)(value >> 24);
            buffer[offset + 1] = (byte)(value >> 16);
            buffer[offset + 2] = (byte)(value >> 8);
            buffer[offset + 3] = (byte)(value >> 0);
        }

    }
}
