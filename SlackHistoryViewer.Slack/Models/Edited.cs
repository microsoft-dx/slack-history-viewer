using Newtonsoft.Json;

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
