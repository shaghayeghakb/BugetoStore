//using Microsoft.AspNetCore.Cryptography.KeyDerivation;
namespace bugeto_stor.common
{
    internal class KeyDerivationPrf
    {
        public static object HMACSHA1 { get; internal set; }
        public static object HMACSHA256 { get; internal set; }
        public static object HMACSHA512 { get; internal set; }
    }
}