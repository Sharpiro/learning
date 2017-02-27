using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Text;

namespace SharpiroTweeter
{
    public interface ITwitterRequestBuilder
    {
        HttpRequestMessage GetAuthorizedMessage(HttpMethod method, string endpoint);
    }

    public class SingleUserRequestBuilder : ITwitterRequestBuilder
    {
        private const string _baseUrl = "https://api.twitter.com";
        private readonly AuthItems _authItems;

        public SingleUserRequestBuilder(AuthItems authItems)
        {
            _authItems = authItems ?? throw new ArgumentNullException(nameof(authItems));
        }

        public HttpRequestMessage GetAuthorizedMessage(HttpMethod method, string endpoint)
        {
            var url = $"{_baseUrl}{endpoint}";
            var requestMessage = new HttpRequestMessage(method, url);
            var oAuthHeaderString = _authItems.GetOAuthHeadersString(method, url);
            requestMessage.Headers.Add("Authorization", $"OAuth {oAuthHeaderString}");
            requestMessage.Headers.Add("Accept-Encoding", "gzip");
            return requestMessage;
        }
    }

    public class BearerRequestBuilder : ITwitterRequestBuilder
    {
        private readonly AuthItems _authItems;

        public BearerRequestBuilder(AuthItems authItems)
        {
            _authItems = authItems ?? throw new ArgumentNullException(nameof(authItems));
        }

        public HttpRequestMessage GetAuthorizedMessage(HttpMethod method, string url)
        {
            _authItems.BearerToken = _authItems.BearerToken == null ? new AuthItem(GetBearerToken()) : _authItems.BearerToken;
            var requestMessage = new HttpRequestMessage(method, url);
            requestMessage.Headers.Add("Authorization", $"Bearer {_authItems.BearerToken.Value}");
            requestMessage.Headers.Add("Accept-Encoding", "gzip");
            return requestMessage;
        }



        private string GetBearerToken()
        {
            using (var client = new HttpClient())
            {
                var key = GetKey();

                const string bearerUrl = "https://api.twitter.com/oauth2/token";
                const string content = "grant_type=client_credentials";

                var httpContent = new StringContent(content);
                httpContent.Headers.Clear();
                httpContent.Headers.Add("Content-Type", "application/x-www-form-urlencoded;charset=UTF-8");
                var requestMessage = new HttpRequestMessage(HttpMethod.Post, bearerUrl) { Content = httpContent };
                requestMessage.Headers.Add("Authorization", $"Basic {key}");

                var responseData = client.SendAsync(requestMessage).Result.Content.ReadAsStringAsync().Result;
                var responseObject = JObject.Parse(responseData);
                var token = (string)responseObject["access_token"];
                if (token == null) throw new NullReferenceException("The token returned was null");
                return token;
            }

            string GetKey()
            {
                var concat = $"{_authItems.ConsumerKey?.Value}:{_authItems.ConsumerSecret?.Value}";
                var bytes = Encoding.UTF8.GetBytes(concat);
                var baseEncoded = Convert.ToBase64String(bytes);
                return baseEncoded;
            }
        }
    }
}