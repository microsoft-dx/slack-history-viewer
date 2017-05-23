using Newtonsoft.Json;

namespace SlackHistoryViewer.Slack.Models
{
    public class Purpose
    {
        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("creator")]
        public string Creator { get; set; }

        [JsonProperty("last_set")]
        public int? LastSet { get; set; }
    }
}
