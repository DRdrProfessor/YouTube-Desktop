﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouTube_Desktop.Core.Models.Media
{
    public interface IMediaFormat
    {
        /// <summary>
        /// The itag of the media stream.
        /// </summary>
        /// 
        [JsonProperty("itag")]
        int ITag { get; set; }
        /// <summary>
        /// The url of the media stream.
        /// </summary>
        /// 
        [JsonProperty("url")]
        string Url { get; set; }
        /// <summary>
        /// The codec and media type.
        /// </summary>
        /// 
        [JsonProperty("mimeType")]
        string MimeType { get; set; }
        /// <summary>
        /// The bitrate of the media stream.
        /// </summary>
        /// 
        [JsonProperty("bitrate")]
        long Bitrate { get; set; }
        /// <summary>
        /// Last time modified (in ms).
        /// </summary>
        /// 
        [JsonProperty("lastModified")]
        [JsonConverter(typeof(JsonParserSerializationConverter))]
        DateTime LastModified { get; set; } // Needs converter.
        /// <summary>
        /// The content length of the media stream.
        /// </summary>
        /// 
        [JsonProperty("contentLength")]
        long ContentLength { get; set; }
        /// <summary>
        /// The quality type.
        /// </summary>
        /// 
        [JsonProperty("quality")]
        string Quality { get; set; }
        /// <summary>
        /// The projection of the media stream.
        /// </summary>
        /// 
        [JsonProperty("projectionType")]
        string ProjectionType { get; set; }
        /// <summary>
        /// The average bitrate.
        /// </summary>
        /// 
        [JsonProperty("averageBitrate")]
        long AverageBitrate { get; set; }
        /// <summary>
        /// A approx duration of the media stream.
        /// </summary>
        /// 
        [JsonProperty("approxDurationMs")]
        long ApproxDurationMs { get; set; }
        /// <summary>
        /// The signature cipher if available.
        /// </summary>
        /// 
        [JsonProperty("signatureCipher")]
        string SignatureCipher { get; set; }
    }
}