using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SlackHistoryViewer.Slack.Models
{
    public class Member
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("team_id")]
        public string TeamId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("deleted")]
        public bool? Deleted { get; set; }

        [JsonProperty("profile")]
        public Profile Profile { get; set; }

        [JsonProperty("isBot")]
        public bool? Is_bot { get; set; }

        [JsonProperty("updated")]
        public int? Updated { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }

        [JsonProperty("real_name")]
        public string RealName { get; set; }

        [JsonProperty("tz")]
        public string Tz { get; set; }

        [JsonProperty("tz_label")]
        public string TzLabel { get; set; }

        [JsonProperty("tz_offset")]
        public int? TzOffset { get; set; }

        [JsonProperty("is_admin")]
        public bool? IsAdmin { get; set; }

        [JsonProperty("is_owner")]
        public bool? IsOwner { get; set; }

        [JsonProperty("is_primary_owner")]
        public bool? IsPrimaryOwner { get; set; }

        [JsonProperty("is_restricted")]
        public bool? IsRestricted { get; set; }

        [JsonProperty("is_ultra_restricted")]
        public bool? IsUltraRestricted { get; set; }

        [JsonProperty("has_2fa")]
        public bool? Has2Fa { get; set; }

        [JsonProperty("two_factor_type")]
        public string TwoFactorType { get; set; }
    }
}
