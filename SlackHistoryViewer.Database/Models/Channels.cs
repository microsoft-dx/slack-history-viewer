using System.Collections.Generic;

namespace SlackHistoryViewer.Database.Models
{
    public partial class Channels
    {
        public Channels(string channelId, string name)
        {
            Messages = new HashSet<Messages>();

            this.ChannelId = channelId;
            this.Name = name;
        }

        public int Id { get; set; }

        public string ChannelId { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Messages> Messages { get; set; }
    }
}
