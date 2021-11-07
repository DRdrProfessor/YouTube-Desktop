using System.Collections.Generic;
using Newtonsoft.Json;
using YouTubeScrap.Data.Extend;
using YouTubeScrap.Data.Interfaces;
using YouTubeScrap.Util.JSON;

namespace YouTubeScrap.Data.Renderers
{
    [JsonConverter(typeof(JsonPathConverter))]
    public class ThumbnailOverlayEndorsementRenderer : ITrackingParams
    {
        [JsonProperty("text.runs")]
        public List<TextRun> Text { get; set; }
        [JsonProperty("trackingParams")]
        public string TrackingParams { get; set; }
    }
}