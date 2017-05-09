using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace RestoreFromLocal
{
    public class Reaction
    {
        public string name { get; set; }
        public List<string> users { get; set; }
        public int count { get; set; }
    }

    public class Attachment
    {
        public string title { get; set; }
        public string title_link { get; set; }
        public string text { get; set; }
        public string fallback { get; set; }
        public string image_url { get; set; }
        public string from_url { get; set; }
        public int image_width { get; set; }
        public int image_height { get; set; }
        public int image_bytes { get; set; }
        public string service_icon { get; set; }
        public string service_name { get; set; }
        public int id { get; set; }
    }

    public class Edited
    {
        public string user { get; set; }
        public string ts { get; set; }
    }

    public class Message
    {
        public string type { get; set; }
        public string user { get; set; }
        public string text { get; set; }
        public string ts { get; set; }
        public List<Reaction> reactions { get; set; }
        public List<Attachment> attachments { get; set; }
        public Edited edited { get; set; }

        public string GetMd5Hash(string input)
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

    public class RootMessages
    {
        public bool ok { get; set; }
        public List<Message> messages { get; set; }
        public bool has_more { get; set; }
        public bool is_limited { get; set; }
    }
}
