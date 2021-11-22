//using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;

namespace bugeto_stor.common
{
    internal class KeyDerivation
    {
        internal static byte[] Pbkdf2(string password, byte[] salt, uint prf, int itercount, int requestedlength)
        {
            throw new NotImplementedException();
        }
    }
}