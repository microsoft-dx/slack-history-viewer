using System;
using System.Collections.Generic;
using System.Text;

namespace RestoreFromLocal
{
    public class Profile
    {
        public string First_name { get; set; }
        public string Last_name { get; set; }
        public string Image_24 { get; set; }
        public string Image_32 { get; set; }
        public string Image_48 { get; set; }
        public string Image_72 { get; set; }
        public string Image_192 { get; set; }
        public string Image_512 { get; set; }
        public string Image_1024 { get; set; }
        public string Image_original { get; set; }
        public string Avatar_hash { get; set; }
        public string Real_name { get; set; }
        public string Real_name_normalized { get; set; }
        public string Title { get; set; }
        public string Phone { get; set; }
        public string Skype { get; set; }
        public bool? Always_active { get; set; }
        public object Fields { get; set; }
    }
}
