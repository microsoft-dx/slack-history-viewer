using System.Collections.Generic;

namespace RestoreFromLocal
{
    public class RootChannels
    {
        public bool? Ok { get; set; }
        public IEnumerable<Channel> Channels { get; set; }
    }
}
