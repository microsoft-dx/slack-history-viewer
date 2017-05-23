using System.Collections.Generic;
using Newtonsoft.Json;

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
