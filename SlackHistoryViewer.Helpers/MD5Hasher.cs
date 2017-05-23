using System.Security.Cryptography;
using System.Text;

namespace SlackHistoryViewer.Helpers
{
    public class MD5Hasher
    {
        private MD5Hasher()
        {
        }

        public static string GetMd5Hash(string input)
        {
            var data = MD5.Create()
                .ComputeHash(Encoding.UTF8.GetBytes(input));

            var result = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                result.Append(data[i].ToString("x2"));
            }

            return result.ToString();
        }

        public static bool VerifyMd5Hash(string input, string hash)
        {
            return GetMd5Hash(input).Equals(hash);
        }
    }
}
