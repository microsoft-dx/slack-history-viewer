using System;
using System.Collections.Generic;

namespace RestoreFromLocal.Models
{
    public partial class Channels
    {
        public Channels()
        {
            Messages = new HashSet<Messages>();
        }

        public int Id { get; set; }
        public string ChannelId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Messages> Messages { get; set; }
    }
}
