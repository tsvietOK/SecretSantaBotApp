using System;
using System.Security.Cryptography;
using System.Text;

namespace SecretSantaBotApp.Helpers
{
    public static class StringGenerator
    {
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
