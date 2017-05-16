using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;

namespace SlackHistoryViewer.Slack.Models
{
    public class RootMessages
    {
        [JsonProperty("ok")]
        public bool? Ok { get; set; }

        [JsonProperty("messages")]
        public IEnumerable<Message> Messages { get; set; }

        [JsonProperty("has_more")]
        public bool? HasMore { get; set; }

        [JsonProperty("is_limited")]
        public bool? IsLimited { get; set; }
    }
}
