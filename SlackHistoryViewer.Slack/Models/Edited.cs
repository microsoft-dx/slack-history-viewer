using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SlackHistoryViewer.Slack.Models
{
    public class Edited
    {
        [JsonProperty("user")]
        public string User { get; set; }

        [JsonProperty("ts")]
        public string TimeStamp { get; set; }
    }
}
