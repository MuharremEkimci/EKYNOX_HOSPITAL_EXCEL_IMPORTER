using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace EKYNOX_HEI.CORE.Helpers
{
    public class Cryptography
    {
        public static string Encrypt(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(16);

            using var pbkdf2 = new Rfc2898DeriveBytes(
               password,
               salt,
               600000,
               HashAlgorithmName.SHA512
           );

            byte[] hash = pbkdf2.GetBytes(32);

            return $"{Convert.ToBase64String(salt)}.{Convert.ToBase64String(hash)}";
        }

        public static bool VerifyPassword(string password, string storedHash)
        {
            var parts = storedHash.Split('.');

            byte[] salt = Convert.FromBase64String(parts[0]);
            byte[] hash = Convert.FromBase64String(parts[1]);

            using var pbkdf2 = new Rfc2898DeriveBytes(
                password,
                salt,
                600000,
                HashAlgorithmName.SHA512
            );

            byte[] newHash = pbkdf2.GetBytes(32);

            return CryptographicOperations.FixedTimeEquals(
                hash,
                newHash
            );
        }
    }
}
