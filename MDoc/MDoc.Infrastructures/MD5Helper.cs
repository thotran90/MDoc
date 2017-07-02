using System;
using System.Security.Cryptography;
using System.Text;

namespace MDoc.Infrastructures
{
    public static class MD5Helper
    {
        public static string ToMd5(this string s)
        {
            if (string.IsNullOrEmpty(s)) return "";
            using (var md5 = MD5.Create())
            {
                var inputBytes = Encoding.ASCII.GetBytes(s);
                var hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                var sb = new StringBuilder();
                foreach (var t in hashBytes)
                {
                    sb.Append(t.ToString("X2"));
                }
                return sb.ToString();
            }
        }
        /// <summary>
        /// Generate ramdom password
        /// </summary>
        /// <returns></returns>
        public static string GetPassword()
        {
            var builder = new StringBuilder();
            builder.Append(RandomString(4, true));
            builder.Append(RandomNumber(1000, 9999));
            builder.Append(RandomString(2, false));
            return builder.ToString();
        }

        #region Private Method

        /// <summary>
        ///     Generates a random string with the given length
        /// </summary>
        /// <param name="size">Size of the string</param>
        /// <param name="lowerCase">If true, generate lowercase string</param>
        /// <returns>Random string</returns>
        private static string RandomString(int size, bool lowerCase)
        {
            var builder = new StringBuilder();
            var random = new Random();
            char ch;
            for (var i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26*random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }

        private static int RandomNumber(int min, int max)
        {
            var random = new Random();
            return random.Next(min, max);
        }

        #endregion
    }
}