using System.Collections.Generic;
using Newtonsoft.Json;

namespace SlackHistoryViewer.Slack.Models
{
    public class Channel
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("is_channel")]
        public bool? IsChannel { get; set; }

        [JsonProperty("created")]
        public int? Created { get; set; }

        [JsonProperty("creator")]
        public string Creator { get; set; }

        [JsonProperty("is_archived")]
        public bool? IsArchived { get; set; }

        [JsonProperty("is_general")]
        public bool? IsGeneral { get; set; }

        [JsonProperty("name_normalized")]
        public string NameNormalized { get; set; }

        [JsonProperty("is_member")]
        public bool? IsMember { get; set; }

        [JsonProperty("members")]
        public IEnumerable<string> Members { get; set; }

        [JsonProperty("topic")]
        public Topic Topic { get; set; }

        [JsonProperty("purpose")]
        public Purpose Purpose { get; set; }

        [JsonProperty("previous_names")]
        public IEnumerable<string> PreviousNames { get; set; }

        [JsonProperty("num_members")]
        public int? NumMembers { get; set; }
    }
}
