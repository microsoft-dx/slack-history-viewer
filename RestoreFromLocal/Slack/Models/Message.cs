using System;
using System.Collections.Generic;
using System.Text;

namespace RestoreFromLocal
{
    public class Message
    {
        public string Type { get; set; }
        public string User { get; set; }
        public string Text { get; set; }
        public string Ts { get; set; }
        public IEnumerable<Reaction> Reactions { get; set; }
        public IEnumerable<Attachment> Attachments { get; set; }
        public Edited Edited { get; set; }
    }
}
