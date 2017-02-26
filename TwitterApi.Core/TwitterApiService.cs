using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;

namespace TwitterApi.Core
{
    public class TwitterApiService
    {
        private readonly ITwitterRequestBuilder _requestBuilder;

        public TwitterApiService(ITwitterRequestBuilder requestBuilder)
        {
            _requestBuilder = requestBuilder ?? throw new ArgumentNullException(nameof(requestBuilder));
        }

        public string GetTimeLine()
        {
            const string endpoint = "/1.1/statuses/home_timeline.json";

            var requestMessage = _requestBuilder.GetAuthorizedMessage(HttpMethod.Get, endpoint);
            var responseData = GetData(requestMessage);
            var responseObject = JArray.Parse(responseData);
            return responseData;
        }

        private string GetData(HttpRequestMessage requestMessage)
        {
            if (requestMessage == null) throw new ArgumentNullException(nameof(requestMessage));

            using (var client = new HttpClient())
            {
                var response = client.SendAsync(requestMessage).Result;
                var responseData = response.Content.ReadAsStringAsync().Result;
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    throw new Exception($"An error occurred with the request:\n{responseData}");

                var responseObject = JArray.Parse(responseData);
                return responseData;
            }
        }

        public static TwitterApiService CreateSingleUserService(AuthItems authItems)=> new TwitterApiService(new SingleUserRequestBuilder(authItems));
        public static TwitterApiService CreateAppOnlyService(AuthItems authItems)=> new TwitterApiService(new BearerRequestBuilder(authItems));
    }
}