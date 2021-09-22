﻿using System;
using System.Diagnostics;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using YouTubeScrap.Core;
using YouTubeScrap.Core.Youtube;
using YouTubeScrap.Data;
using YouTubeScrap.Data.Video;
using YouTubeScrap.Handlers;
using YouTubeScrap.Util;

namespace YouTubeScrap
{
    /// <summary>
    /// The main communication API for youtube scrapping!
    /// </summary>
    public sealed class YouTubeService : IDisposable
    {
        private JObject _ytResponse;
        public YouTubeService(YoutubeUser ytUser = null)
        {
            Task constructTask = Task.Run(() =>
            {
                NetworkHandler.Construct();
                _ytResponse = ApiDataManager.GetInnertubeData(ytUser);
            });
        }

        public ResponseMetadata GetMeta()
        {
            if (_ytResponse == null) return null;
            ResponseMetadata metadata = JsonConvert.DeserializeObject<ResponseMetadata>(_ytResponse.ToString());
            return metadata;
        }
        public void TestRequester(YoutubeUser youtubeUser = null)
        {
            //ApiRequest apiRequest;
            //HttpResponse httpResponse;
            //apiRequest = YoutubeApiManager.GetApiRequest(ApiRequestType.Home);
            //apiRequest = YoutubeApiManager.GetApiRequest(ApiRequestType.AccountGet);
            //apiRequest = YoutubeApiManager.PrepareApiRequest(ApiRequestType.Video, null, null, "h3fUgOKFMNU");// ZK3U92URi_c
            //Task<HttpResponse> requestTask = Task.Run(async () => await NetworkHandler.MakeRequestAsync(apiRequest, youtubeUser).ConfigureAwait(false));
            //httpResponse = requestTask.Result;
            //JObject propertiesJson = HtmlHandler.ExtractJsonFromHtml(httpResponse.ResponseString, HTMLExtractions.Properties);
            //JObject playerResponseJson = HtmlHandler.ExtractJsonFromHtml(httpResponse.ResponseString, HTMLExtractions.PlayerResponse);
            //JObject responseJson = HtmlHandler.ExtractJsonFromHtml(httpResponse.ResponseString, HTMLExtractions.InitialResponse);
            //var rc = JsonConvert.DeserializeObject<VideoDataSnippet>(response.ToString());
            return;
        }
        public ResponseMetadata GetHome(YoutubeUser youtubeUser = null, string token = null)
        {
            return null;
        }
        public ResponseMetadata GetSearch(string query, YoutubeUser youtubeUser = null, string token = null)
        {
            return null;
        }
        public ResponseMetadata GetVideo(string videoId, YoutubeUser youtubeUser = null)
        {
            //ApiRequest apiRequest;
            //HttpResponse httpResponse;
            //apiRequest = YoutubeApiManager.PrepareApiRequest(ApiRequestType.Video, null, null, videoId);
            //Task<HttpResponse> requestTask = Task.Run(async () => await NetworkHandler.MakeApiRequestAsync(apiRequest, youtubeUser).ConfigureAwait(false));
            //httpResponse = requestTask.Result;

            //JObject propertiesJSON = HtmlHandler.ExtractJsonFromHtml(httpResponse.ResponseString, HTMLExtractions.Properties);
            //JObject playerResponseJSON = HtmlHandler.ExtractJsonFromHtml(httpResponse.ResponseString, HTMLExtractions.PlayerResponse);
            //JObject initialResponseJSON = HtmlHandler.ExtractJsonFromHtml(httpResponse.ResponseString, HTMLExtractions.InitialResponse);

            //VideoDataSnippet videoData = JsonConvert.DeserializeObject<VideoDataSnippet>(playerResponseJSON.ToString());
            return null;
        }

        public void Dispose()
        {
            NetworkHandler.Dispose();
        }
    }
}