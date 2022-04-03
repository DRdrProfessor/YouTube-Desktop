using Newtonsoft.Json;
using YouTubeScrap.Data.Extend.Endpoints;
using YouTubeScrap.Data.Interfaces;

namespace YouTubeScrap.Data.Extend
{
    public class ButtonRenderer : ITrackingParams
    {
        [JsonProperty("style")]
        public string Style { get; set; }
        [JsonProperty("size")]
        public string Size { get; set; }
        [JsonProperty("isDisabled")]
        public bool IsDisabled { get; set; }
        [JsonProperty("icon")]
        public string Icon { get; set; }
        [JsonProperty("iconPosition")]
        public string IconPosition { get; set; }
        [JsonProperty("accessibility")]
        public Accessibility AccessibilityLabel { get; set; }
        [JsonProperty("tooltip")]
        public string Tooltip { get; set; }
        [JsonProperty("text")]
        public TextElement Text { get; set; }
        [JsonProperty("serviceEndpoint")]
        public ServiceEndpoint ServiceEndpoint { get; set; }
        [JsonProperty("navigationEndpoint")]
        public NavigationEndpoint NavigationEndpoint { get; set; }
        [JsonProperty("trackingParams")]
        public string TrackingParams { get; set; }
        [JsonProperty("targetId")]
        public string TargetId { get; set; }
        [JsonProperty("command")]
        public Command Command { get; set; }
    }
}