using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SlackHistoryViewer.Slack.Models
{
    public class Reaction
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("users")]
        public IEnumerable<string> Users { get; set; }

        [JsonProperty("count")]
        public int? Count { get; set; }
    }
}
