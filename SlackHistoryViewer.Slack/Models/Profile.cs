using Newtonsoft.Json;

namespace SlackHistoryViewer.Slack.Models
{
    public class Profile
    {
        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("image_24")]
        public string Image24 { get; set; }

        [JsonProperty("image_32")]
        public string Image32 { get; set; }

        [JsonProperty("image_48")]
        public string Image48 { get; set; }

        [JsonProperty("image_72")]
        public string Image72 { get; set; }

        [JsonProperty("image_192")]
        public string Image192 { get; set; }

        [JsonProperty("image_512")]
        public string Image512 { get; set; }

        [JsonProperty("image_1024")]
        public string Image1024 { get; set; }

        [JsonProperty("image_original")]
        public string ImageOriginal { get; set; }

        [JsonProperty("avatar_hash")]
        public string AvatarHash { get; set; }

        [JsonProperty("real_name")]
        public string RealName { get; set; }

        [JsonProperty("real_name_normalized")]
        public string RealNameNormalized { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("skype")]
        public string Skype { get; set; }

        [JsonProperty("always_active")]
        public bool? AlwaysActive { get; set; }
    }
}
