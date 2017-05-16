using System;
using System.Collections.Generic;
using System.Text;

namespace RestoreFromLocal
{
    public class Attachment
    {
        public string Title { get; set; }
        public string Title_link { get; set; }
        public string Text { get; set; }
        public string Fallback { get; set; }
        public string Image_url { get; set; }
        public string From_url { get; set; }
        public int? Image_width { get; set; }
        public int? Image_height { get; set; }
        public int? Image_bytes { get; set; }
        public string Service_icon { get; set; }
        public string Service_name { get; set; }
        public int? Id { get; set; }
    }
}
