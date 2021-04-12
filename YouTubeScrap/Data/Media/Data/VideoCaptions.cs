﻿using System.Collections.Generic;
using Newtonsoft.Json;

namespace YouTubeScrap.Data.Media.Data
{
    public class VideoCaptions
    {
        [JsonProperty("playerCaptionsRenderer")]
        public CaptionsRenderer PlayerCaptionsRenderer { get; set; }
        [JsonProperty("playerCaptionsTracklistRenderer")]
        public PlayerCaptionsTrackList PlayerCaptionsTrackList { get; set; }
    }
    public struct CaptionsRenderer
    {
        [JsonProperty("baseUrl")]
        public string BaseUrl { get; set; }
        [JsonProperty("visibility")]
        public string Visibility { get; set; }
    }
    public struct PlayerCaptionsTrackList
    {
        [JsonProperty("captionTracks")]
        public List<CaptionTrack> CaptionTracks { get; set; }
        [JsonProperty("audioTracks")]
        public List<CaptionAudioTrack> AudioTracks { get; set; }
        [JsonProperty("defaultAudioTrackIndex")]
        public int DefaultAudioTrackIndex { get; set; }
    }
}