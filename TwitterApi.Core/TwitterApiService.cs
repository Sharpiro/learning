using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using SharpiroTweeter.Models;

namespace SharpiroTweeter
{
    public class TwitterApiService
    {
        private readonly ITwitterRequestBuilder _requestBuilder;

        public TwitterApiService(ITwitterRequestBuilder requestBuilder)
        {
            _requestBuilder = requestBuilder ?? throw new ArgumentNullException(nameof(requestBuilder));
        }

        public async Task<IEnumerable<Tweet>> GetTimeLine()
        {
            const string endpoint = "/1.1/statuses/home_timeline.json";
            var requestMessage = _requestBuilder.GetAuthorizedMessage(HttpMethod.Get, endpoint);
            var responseData = await GetData(requestMessage);
            try
            {
                var responseArray = JArray.Parse(responseData);
                var timeline = responseArray.Select(i => new Tweet
                {
                    Id = (long)i["id"],
                    Text = (string)i["text"],
                    Hashtags = i.SelectToken("entities.hashtags").Select(t => (string)t["text"]),
                    UserMentions = i.SelectToken("entities.user_mentions").Select(t => new User
                    {
                        Id = (int)t["id"],
                        Name = (string)t["name"],
                        ScreenName = (string)t["screen_name"]
                    }),
                    Urls = i.SelectToken("entities.urls").Select(t => (string)t["url"]),
                    User = new User
                    {
                        Id = (long)i.SelectToken("user.id"),
                        Name = (string)i.SelectToken("user.name"),
                        ScreenName = (string)i.SelectToken("user.screen_name")
                    }
                });
                return timeline;
            }
            catch (Exception ex)
            {
                throw new JsonException("An error occurred parsing json", ex);
            }
        }

        private async Task<string> GetData(HttpRequestMessage requestMessage)
        {
            if (requestMessage == null) throw new ArgumentNullException(nameof(requestMessage));

            using (var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip }))
            {
                var response = await client.SendAsync(requestMessage);
                var responseData = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    throw new Exception($"An error occurred with the request:\n{responseData}");
                return responseData;
            }
        }

        public static TwitterApiService CreateSingleUserService(AuthItems authItems) => new TwitterApiService(new SingleUserRequestBuilder(authItems));
        public static TwitterApiService CreateAppOnlyService(AuthItems authItems) => new TwitterApiService(new BearerRequestBuilder(authItems));
    }
}