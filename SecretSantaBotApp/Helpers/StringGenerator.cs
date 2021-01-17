using System;
using System.Security.Cryptography;
using System.Text;

namespace SecretSantaBotApp.Helpers
{
    /// <summary>
    /// Provides static methods for string generation.
    /// </summary>
    public static class StringGenerator
    {
        /// <summary>
        /// Generates random string with the specified length.
        /// </summary>
        /// <param name="length">Length of the generated string.</param>
        /// <returns>Randomly generated string of the specified length.</returns>
        public static string RandomString(int length)
        {
            const string ValidSymbols = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                byte[] uintBuffer = new byte[sizeof(uint)];

                while (length-- > 0)
                {
                    rng.GetBytes(uintBuffer);
                    uint num = BitConverter.ToUInt32(uintBuffer, 0);
                    res.Append(ValidSymbols[(int)(num % (uint)ValidSymbols.Length)]);
                }
            }

            return res.ToString();
        }
    }
}
