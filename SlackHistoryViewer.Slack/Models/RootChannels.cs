using Newtonsoft.Json;
using System.Collections.Generic;

namespace SlackHistoryViewer.Slack.Models
{
    public class RootChannels
    {
        [JsonProperty("ok")]
        public bool? Ok { get; set; }

        [JsonProperty("channels")]
        public IEnumerable<Channel> Channels { get; set; }
    }
}
