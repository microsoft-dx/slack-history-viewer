using Newtonsoft.Json;

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

        [JsonProperty("is_bot")]
        public bool? IsBot { get; set; }

        [JsonProperty("updated")]
        public int? Updated { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }

        [JsonProperty("real_name")]
        public string RealName { get; set; }

        [JsonProperty("tz")]
        public string TimeZone { get; set; }

        [JsonProperty("tz_label")]
        public string TimeZoneLabel { get; set; }

        [JsonProperty("tz_offset")]
        public int? TimeZoneOffset { get; set; }

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
        public bool? HasTwoFactorAuthentication { get; set; }

        [JsonProperty("two_factor_type")]
        public string TwoFactorType { get; set; }
    }
}
