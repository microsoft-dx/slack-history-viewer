using System.Collections.Generic;
using Newtonsoft.Json;

namespace SlackHistoryViewer.Slack.Models
{
    public class RootMembers
    {
        [JsonProperty("ok")]
        public bool? Ok { get; set; }

        [JsonProperty("members")]
        public IEnumerable<Member> Members { get; set; }

        [JsonProperty("cache_ts")]
        public int CacheTimeStamp { get; set; }
    }
}
