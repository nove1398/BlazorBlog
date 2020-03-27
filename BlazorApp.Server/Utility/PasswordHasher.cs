using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp.Server.Utility
{
    public static class PasswordHasher
    {
        /// <summary>
        /// Size of salt.
        /// </summary>
        private const int SaltSize = 16;

        /// <summary>
        /// Creates a hash from a password with 10000 iterations
        /// </summary>
        /// <param name="password">The password.</param>
        /// <returns>The hash.</returns>
        public static string GenerateSalt()
        {
            using RNGCryptoServiceProvider saltGenerator = new RNGCryptoServiceProvider();
            byte[] salt = new byte[SaltSize];
            saltGenerator.GetBytes(salt);
            return Convert.ToBase64String(salt);
        }

        /// <summary>
        /// Converts byte array to hex string
        /// </summary>
        /// <param name="ba">The byte array to convert to string</param>
        /// <returns>A hex string of the input byte array</returns>
        private static string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }

        /// <summary>
        /// Hashes password with salt that is given
        /// </summary>
        /// <param name="password">The password to hash</param>
        /// <param name="salt">The provided salt to add in the hash</param>
        /// <returns></returns>
        public static string HashPass(string password, string salt)
        {
            using SHA256Managed shaString = new SHA256Managed();
            byte[] bytes = Encoding.UTF8.GetBytes(password + salt);
            byte[] hashed = shaString.ComputeHash(bytes);
            return ByteArrayToString(hashed);
        }

        /// <summary>
        /// Verifies a password against a hash.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <param name="hashedPassword">The hash.</param>
        /// <param name="salt">The salt.</param>
        /// <returns>true if verified</returns>
        public static bool VerifyPass(string password, string hashedPassword, string salt)
        {
            string currentPass = HashPass(password, salt);
            return string.Equals(currentPass, hashedPassword, StringComparison.Ordinal);
        }
    }
}
