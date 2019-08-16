using System;
using System.Security.Cryptography;

namespace OAuthDb
{
    public class UserUTIL
    {
        // Password hashing
        public String PasswordHash(String pwd)
        {
            return this.GenerateHash(pwd, this.CreateSalt(OAuthDbCONST.PWD_SALTLENGTH));
        }

        private string CreateSalt(int size)
        {
            //Generate a cryptographic random number. >> Not in Use
            // RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();

            String salt = OAuthDbCONST.PWD_SALTKEY;
            byte[] buff = new byte[size];

            buff = System.Text.Encoding.ASCII.GetBytes(salt);
            return Convert.ToBase64String(buff);
        }


        private string GenerateHash(string input, string salt)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(input + salt);
            SHA256Managed sHA256ManagedString = new SHA256Managed();
            byte[] hash = sHA256ManagedString.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        public bool IsPassword(string plainTextInput, string hashedInput)
        {
            string newHashedPin = GenerateHash(plainTextInput, this.CreateSalt(OAuthDbCONST.PWD_SALTLENGTH));
            return newHashedPin.Equals(hashedInput);
        }
        // End Password hashing

        // Encryption - Decryption

        // End Encryption - Decryption
    }
}
