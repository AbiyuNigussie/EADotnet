﻿using EventAddis.Repository;
using Microsoft.AspNetCore.Identity;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;

namespace EventAddis.Services
{
    public class HashService : IHashService
    {
        private byte Version => 1;
        private int SaltSize { get; } = 128 / 8; // 128 bits
        private HashAlgorithmName myHashAlgorithm { get; } = HashAlgorithmName.SHA256;

        public Dictionary<string, string> HashPassword(string password)
        {
            if (password == null)
                throw new ArgumentNullException(nameof(password));

            // The salt must be unique for each password
            byte[] salt = GenerateSalt(SaltSize);
            byte[] hash = HashPasswordWithSalt(password, salt);

            var inArray = new byte[1 + SaltSize + hash.Length];
            inArray[0] = Version;
            Buffer.BlockCopy(salt, 0, inArray, 1, SaltSize);
            Buffer.BlockCopy(hash, 0, inArray, 1 + SaltSize, hash.Length);

            Dictionary<string, string> userCredentials = new Dictionary<string, string>();
            userCredentials.Add("salt", Convert.ToBase64String(salt));
            userCredentials.Add("hash", Convert.ToBase64String(inArray));

            return userCredentials;
        }
        private byte[] GenerateSalt(int byteLength)
        {
            using (var cryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                var data = new byte[byteLength];
                cryptoServiceProvider.GetBytes(data);
                return data;
            }
        }

        private byte[] HashPasswordWithSalt(string password, byte[] salt)
        {
            byte[] hash;
            using (var hashAlgorithm = HashAlgorithm.Create(myHashAlgorithm.Name))
            {
                byte[] input = Encoding.UTF8.GetBytes(password);
                hashAlgorithm.TransformBlock(salt, 0, salt.Length, salt, 0);
                hashAlgorithm.TransformFinalBlock(input, 0, input.Length);
                hash = hashAlgorithm.Hash;
            }

            return hash;
        }


        public PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string password)
        {
            if (password == null)
                throw new ArgumentNullException(nameof(password));

            if (hashedPassword == null)
                return PasswordVerificationResult.Failed;

            Span<byte> numArray = Convert.FromBase64String(hashedPassword);
            if (numArray.Length < 1)
                return PasswordVerificationResult.Failed;

            byte version = numArray[0];
            if (version > Version)
                return PasswordVerificationResult.Failed;


            var salt = numArray.Slice(1, SaltSize).ToArray();
            var bytes = numArray.Slice(1 + SaltSize).ToArray();

            var hash = HashPasswordWithSalt(password, salt);

            if (FixedTimeEquals(hash, bytes))
                return PasswordVerificationResult.Success;

            return PasswordVerificationResult.Failed;
        }

       
        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
        private static bool FixedTimeEquals(byte[] left, byte[] right)
        {
            // NoOptimization because we want this method to be exactly as non-short-circuiting as written.
            // NoInlining because the NoOptimization would get lost if the method got inlined.
            if (left.Length != right.Length)
            {
                return false;
            }

            int length = left.Length;
            int accum = 0;

            for (int i = 0; i < length; i++)
            {
                accum |= left[i] - right[i];
            }

            return accum == 0;
        }
    }
}