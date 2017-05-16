using Newtonsoft.Json;
using SlackHistoryViewer.Slack.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SlackHistoryViewer.Slack.Models
{
    public class Message
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("user")]
        public string User { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("ts")]
        public string TimeStamp { get; set; }

        [JsonProperty("reactions")]
        public IEnumerable<Reaction> Reactions { get; set; }

        [JsonProperty("attachaments")]
        public IEnumerable<Attachment> Attachments { get; set; }

        [JsonProperty("edited")]
        public Edited Edited { get; set; }
    }
}
