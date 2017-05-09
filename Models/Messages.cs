using System;
using System.Collections.Generic;

namespace RestoreFromLocal.Models
{
    public partial class Messages
    {
        public int Id { get; set; }
        public string MessageId { get; set; }
        public int UserId { get; set; }
        public int ChannelId { get; set; }
        public string JsonData { get; set; }

        public virtual Channels Channel { get; set; }
        public virtual Users User { get; set; }
    }
}
