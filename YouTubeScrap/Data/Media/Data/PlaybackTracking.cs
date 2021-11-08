﻿using Newtonsoft.Json;
using YouTubeScrap.Util;

namespace YouTubeScrap.Data.Media.Data
{
    public class PlaybackTracking
    {
        [JsonProperty("videostatsPlaybackUrl")]
        public string VideoStatsPlaybackUrl { get; set; }
        [JsonProperty("videostatsDelayplayUrl")]
        public string VideoStatsDelayplayUrl { get; set; }
        [JsonProperty("videostatsWatchtimeUrl")]
        public string VideoStatsWatchtimeUrl { get; set; }
        [JsonProperty("ptrackingUrl")]
        public string PTrackingUrl { get; set; }
        [JsonProperty("qoeUrl")]
        public string QoeUrl { get; set; }
        [JsonProperty("setAwesomeUrl")]
        public UrlAttribute SetAwesomeUrl { get; set; }
        [JsonProperty("atrUrl")]
        public UrlAttribute AtrUrl { get; set; }
        [JsonProperty("youtubeRemarketingUrl")]
        public UrlAttribute YoutubeRemarketingUrl { get; set; }
        [JsonProperty("googleRemarketingUrl")]
        public UrlAttribute GoogleRemarketingUrl { get; set; }
    }
    public struct UrlAttribute
    {
        [JsonProperty("baseUrl")]
        public string BaseUrl { get; set; }
        [JsonProperty("elapsedMediaTimeSeconds")]
        public long ElapsedMediaTimeSeconds { get; set; }
    }
}