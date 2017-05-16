using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SlackHistoryViewer.Slack.Models
{
    public class Topic
    {
        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("creator")]
        public string Creator { get; set; }

        [JsonProperty("last_set")]
        public int? LastSet { get; set; }
    }
}
