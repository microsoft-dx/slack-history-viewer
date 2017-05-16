using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace RestoreFromLocal
{
    class MD5Hasher
    {
        private MD5Hasher() { }

        public static string GetMd5Hash(string input)
        {
            MD5 md5Hash = MD5.Create();
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
    }
}
