using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SlackHistoryViewer.Slack.Models
{
    public class Attachment
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("title_link")]
        public string TitleLink { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("fallback")]
        public string Fallback { get; set; }

        [JsonProperty("image_url")]
        public string ImageUrl { get; set; }

        [JsonProperty("from_url")]
        public string FromUrl { get; set; }

        [JsonProperty("image_width")]
        public int? ImageWidth { get; set; }

        [JsonProperty("image_height")]
        public int? ImageHeight { get; set; }

        [JsonProperty("image_bytes")]
        public int? ImageBytes { get; set; }

        [JsonProperty("service_icon")]
        public string ServiceIcon { get; set; }

        [JsonProperty("service_name")]
        public string ServiceName { get; set; }

        [JsonProperty("id")]
        public int? Id { get; set; }
    }
}
