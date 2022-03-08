using System.Collections.Generic;
using Newtonsoft.Json;
using YouTubeScrap.Data.Extend;
using YouTubeScrap.Data.Extend.Endpoints;
using YouTubeScrap.Data.Interfaces;
using YouTubeScrap.Util.JSON;

namespace YouTubeScrap.Data.Renderers
{
    public class RichShelfRenderer : ITrackingParams
    {
        [JsonProperty("title")]
        public TextElement Title { get; set; }
        [JsonProperty("contents")]
        [JsonConverter(typeof(JsonContentConverter))]
        public List<object> Contents { get; set; }
        public string TrackingParams { get; set; }
        [JsonProperty("badges")]
        public List<Badge> Badges { get; set; }

        [JsonProperty("subtitle")]
        public TextElement Subtitle { get; set; } = new TextElement();
        [JsonProperty("thumbnails")]
        public List<UrlImage> Thumbnails { get; set; }
        [JsonProperty("endpoint")]
        public Endpoint Endpoint { get; set; }
        [JsonProperty("impressionEndpoints")]
        public List<ImpressionEndpoint> ImpressionEndpoints { get; set; }
        [JsonProperty("menu")]
        public ActionMenu Menu { get; set; }
        [JsonProperty("showMoreButton")]
        public ButtonRenderer ShowMoreButtonRenderer { get; set; }

        public ContentRender SelectedItem
        {
            get => _selectedItem;
            set => _selectedItem = value;
        }
        private ContentRender _selectedItem;
    }
}