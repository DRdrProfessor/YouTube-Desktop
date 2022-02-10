using System.Collections.Generic;
using Newtonsoft.Json;
using YouTubeScrap.Data.Extend;
using YouTubeScrap.Util.JSON;

namespace YouTubeScrap.Data.Renderers
{
    [JsonConverter(typeof(JsonPathConverter))]
    public class GuideCollapsibleEntryRenderer
    {
        [JsonProperty("expanderItem.guideEntryRenderer")]
        public GuideEntryRenderer ExpanderItem { get; set; }
        [JsonProperty("expandableItems")]
        [JsonConverter(typeof(JsonGuideConverter))]
        public List<object> ExpandableItems { get; set; }
        [JsonProperty("collapserItem.guideEntryRenderer")]
        public GuideEntryRenderer CollapserItem { get; set; }
    }
}