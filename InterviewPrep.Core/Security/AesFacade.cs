using System;
using System.IO;
using System.Security.Cryptography;

namespace InterviewPrep.Core.Security
{
    public class AesFacade
    {
        private readonly byte[] _iv;
        private readonly byte[] _key;

        public AesFacade(byte[] key, byte[] iv)
        {
            if (key == null || key.Length <= 0) throw new ArgumentNullException(nameof(key));
            if (iv == null || iv.Length <= 0) throw new ArgumentNullException(nameof(iv));

            _key = key;
            _iv = iv;
        }

        public byte[] Encrypt(string plainText)
        {
            if (string.IsNullOrEmpty(plainText)) throw new ArgumentNullException(nameof(plainText));

            byte[] encrypted;
            using (var aesAlg = new AesManaged { Key = _key, IV = _iv })
            {
                var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (var msEncrypt = new MemoryStream())
                using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (var swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(plainText);
                    }
                    encrypted = msEncrypt.ToArray();
                }
            }
            return encrypted;
        }

        public string Decrypt(byte[] cipherText)
        {
            if (cipherText == null || cipherText.Length <= 0) throw new ArgumentNullException(nameof(cipherText));

            string plaintext = null;
            using (var aesAlg = new AesManaged { Key = _key, IV = _iv })
            {
                var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (var msDecrypt = new MemoryStream(cipherText))
                using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                using (var srDecrypt = new StreamReader(csDecrypt))
                {
                    plaintext = srDecrypt.ReadToEnd();
                }
            }
            return plaintext;
        }

        public static byte[] GetRandomIV()
        {
            using (AesManaged myAes = new AesManaged())
            {
                return myAes.IV;
            }
        }

        public static byte[] GetRandomKey()
        {
            var random = new Random();
            var key = new byte[32];

            random.NextBytes(key);

            return key;
        }

        public static string GetSaltedKey(string password, int hashAndSaltSize = 16, int iterations = 10000)
        {
            if (!IsPowerOfTwo(hashAndSaltSize)) throw new ArgumentException($"{nameof(hashAndSaltSize)} must be a power of 2");

            var rngCryptoProvider = new RNGCryptoServiceProvider();
            var salt = new byte[hashAndSaltSize];
            rngCryptoProvider.GetBytes(salt);

            var hash = new Rfc2898DeriveBytes(password, salt, iterations).GetBytes(hashAndSaltSize);

            var hashBytes = new byte[hashAndSaltSize + hashAndSaltSize];
            Array.Copy(salt, 0, hashBytes, 0, hashAndSaltSize);
            Array.Copy(hash, 0, hashBytes, hashAndSaltSize, hashAndSaltSize);

            var passwordHash = Convert.ToBase64String(hashBytes);

            return passwordHash;
        }

        public static AesFacade GetAesFacade()
        {
            throw new NotImplementedException();
        }

        public static bool CheckSaltedKey(string password, string saltedKey, int hashAndSaltSize = 16, int iterations = 10000)
        {
            if (!IsPowerOfTwo(hashAndSaltSize)) throw new ArgumentException($"{nameof(hashAndSaltSize)} must be a power of 2");

            var hashBytes = Convert.FromBase64String(saltedKey);
            var salt = new byte[hashAndSaltSize];
            Array.Copy(hashBytes, 0, salt, 0, hashAndSaltSize);

            var hash = new Rfc2898DeriveBytes(password, salt, iterations).GetBytes(hashAndSaltSize);

            for (var i = 0; i < hashAndSaltSize; i++)
            {
                if (hashBytes[i + hashAndSaltSize] != hash[i]) return false;
            }
            return true;
        }

        private static bool IsPowerOfTwo(int x)
        {
            return x != 0 && (x & (x - 1)) == 0;
        }
    }
}