﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using YouTubeScrap.Data.Extend;
using YouTubeScrap.Util;

namespace YouTubeScrap.Data.Media.Data
{
    public class Microformat
    {
        [JsonProperty("thumbnail")]
        public List<UrlImage> Thumbnails { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("description")]
        public string Desciption { get; set; }
        [JsonProperty("lengthSeconds")]
        public long LengthInSeconds { get; set; }
        [JsonProperty("ownerProfileUrl")]
        public string OwnerProfileUrl { get; set; }
        [JsonProperty("externalChannelId")]
        public string ExternalChannelId { get; set; }
        [JsonProperty("isFamilySafe")]
        public bool IsFamilySafe { get; set; }
        [JsonProperty("availableCountries")]
        public List<string> AvailableCountries { get; set; }
        [JsonProperty("isUnlisted")]
        public bool IsUnlisted { get; set; }
        [JsonProperty("hasYpcMetadata")]
        public bool HasYpcMetadata { get; set; }
        [JsonProperty("viewCount")]
        public long ViewCount { get; set; }
        [JsonProperty("category")]
        public string Category { get; set; }
        [JsonProperty("publishDate")]
        public DateTime PublishDate { get; set; }
        [JsonProperty("ownerChannelName")]
        public string OwnerChannelName { get; set; }
        [JsonProperty("liveBroadcastDetails")]
        public BroadcastDetails LiveBroadcastDetails { get; set; }
        [JsonProperty("uploadDate")]
        public DateTime UploadDate { get; set; }
    }
    public struct BroadcastDetails
    {
        [JsonProperty("isLiveNow")]
        public bool IsLiveNow { get; set; }
        [JsonProperty("startTimestamp")]
        public DateTime StartTimeStamp { get; set; }
        [JsonProperty("endTimestamp")]
        public DateTime EndTimeStamp { get; set; }
    }
}