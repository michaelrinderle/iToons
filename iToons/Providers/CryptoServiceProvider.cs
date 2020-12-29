using System;
using System.Security.Cryptography;

namespace iToons.Providers
{
    public class CryptoServiceProvider
    {
        private byte[] Salt { get; set; }

        public CryptoServiceProvider()
        {
            CreateSalt();
        }

        private void CreateSalt()
        {
            new RNGCryptoServiceProvider().GetBytes(Salt = new byte[16]);
        }

        private byte[] CreateHashValue(string password)
        {
            var pbkdf2 = new Rfc2898DeriveBytes(password, Salt, 100000);
            return pbkdf2.GetBytes(20);
        }

        private byte[] CombineSaltAndPassword(byte[] hash)
        {
            byte[] hashBytes = new byte[36];
            Array.Copy(Salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);
            return hashBytes;
        }

        private string ConvertSaltedHashToString(byte[] hashBytes)
        {
            return Convert.ToBase64String(hashBytes);
        }

        public string CreatePasswordHash(string password)
        {
            var hv = CreateHashValue(password);
            var hb = CombineSaltAndPassword(hv);
            var hash = ConvertSaltedHashToString(hb);
            return hash;
        }

        public static bool VerifyHashString(string password, string hashString)
        {
            // extract bytes
            byte[] hashBytes = Convert.FromBase64String((hashString));
            // get salt 
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            // verify hash to password
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(20);
            // comparing results
            for (int i = 0; i < 20; i++)
            {
                if (hashBytes[i + 16] != hash[i])
                    return false;
            }
            return true;
        }
    }
}