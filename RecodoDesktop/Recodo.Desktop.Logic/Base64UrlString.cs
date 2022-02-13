using System;
using System.Security.Cryptography;
using System.Text;

namespace Recodo.Desktop.Logic
{
    public class Base64UrlString
    {
        public static string RandomBase64UrlString(uint length)
        {
            var rng = RandomNumberGenerator.Create();
            var bytes = new byte[length];
            rng.GetBytes(bytes);
            return Base64UrlEncodeNoPadding(bytes);
        }

        public static byte[] Sha256(string inputString)
        {
            using var sha256 = SHA256.Create();
            return sha256.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }

        public static string Base64UrlEncodeNoPadding(byte[] buffer)
        {
            return Convert.ToBase64String(buffer)
                    .TrimEnd('=')
                    .Replace('+', '-')
                    .Replace('/', '_');
        }
    }
}