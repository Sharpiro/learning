using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using Tweetinvi.Security.System.Security.Cryptography;
using static TwitterApi.Core.Internals.Extensions;

namespace TwitterApi.Core
{
    public class AuthItems : IEnumerable<AuthItem>
    {
        public AuthItem ConsumerKey { get; }
        public AuthItem ConsumerSecret { get; }
        public AuthItem BearerToken { get; set; }
        public AuthItem BearerTokenSecret { get; }

        public AuthItems(string consumerKey, string consumerSecret, string bearerToken = null, string bearerTokenSecret = null)
        {
            if (string.IsNullOrEmpty(consumerKey)) throw new ArgumentNullException(nameof(consumerKey));
            if (string.IsNullOrEmpty(consumerSecret)) throw new ArgumentNullException(nameof(consumerSecret));

            ConsumerKey = new AuthItem(consumerKey, isRequiredForSecretKey: false, isRequiredForSignature: true, headerName: "oauth_consumer_key");
            ConsumerSecret = new AuthItem(consumerSecret, isRequiredForSecretKey: true);
            if (bearerToken != null)
                BearerToken = new AuthItem(bearerToken, isRequiredForSecretKey: false, isRequiredForSignature: true, headerName: "oauth_token");
            if (bearerTokenSecret != null)
                BearerTokenSecret = new AuthItem(bearerTokenSecret, isRequiredForSecretKey: true);
        }

        public string GetOAuthHeadersString(HttpMethod method, string url)
        {
            var oAuthHeaders = GetOAuthHeaders(method, url);
            var builder = new StringBuilder();
            foreach (var header in oAuthHeaders)
            {
                if (builder.Length > 0)
                    builder.Append(",");
                builder.Append($"{header.Key}={header.Value}");
            }
            return builder.ToString();
        }

        public ImmutableList<KeyValuePair<string, string>> GetOAuthHeaders(HttpMethod method, string url)
        {
            if (string.IsNullOrEmpty(url)) throw new ArgumentNullException(nameof(url));

            string oauthNonce = new Random().Next(123400, 9999999).ToString(CultureInfo.InvariantCulture);
            var dateTime = DateTime.UtcNow;
            TimeSpan ts = dateTime - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            string oauthTimestamp = Convert.ToInt64(ts.TotalSeconds).ToString(CultureInfo.InvariantCulture);

            var oauthHeaders = GetOauthHeaders();
            var signature = GetSignature(method, url, oauthHeaders);

            return oauthHeaders.Add(CreateKvp("oauth_signature", signature));

            ImmutableList<KeyValuePair<string, string>> GetOauthHeaders()
            {
                return ImmutableList.Create
                (
                    CreateKvp(ConsumerKey.HeaderName, ConsumerKey.Value),
                    CreateKvp("oauth_nonce", oauthNonce),
                    CreateKvp("oauth_signature_method", "HMAC-SHA1"),
                    CreateKvp("oauth_timestamp", oauthTimestamp),
                    CreateKvp(BearerToken.HeaderName, BearerToken.Value),
                    CreateKvp("oauth_version", "1.0")
                );
            }
        }

        public string GetSignature(HttpMethod method, string url, IEnumerable<KeyValuePair<string, string>> oauthHeaders)
        {
            if (method == null) throw new ArgumentNullException(nameof(method));
            if (string.IsNullOrEmpty(url)) throw new ArgumentNullException(nameof(url));

            var oAuthRequest = getOAuthRequest();
            var oAuthSecretKey = getOAuthSecretKey();
            return UrlEncode(Convert.ToBase64String(new HMACSHA1Generator().ComputeHash(oAuthRequest, oAuthSecretKey, Encoding.UTF8)));

            string getOAuthRequest()
            {
                var urlParametersFormatted = new StringBuilder();
                foreach (var param in oauthHeaders)
                {
                    if (urlParametersFormatted.Length > 0)
                    {
                        urlParametersFormatted.Append("&");
                    }

                    urlParametersFormatted.Append(string.Format("{0}={1}", param.Key, param.Value));
                }
                return $"{method}&{UrlEncode(url)}&{UrlEncode(urlParametersFormatted.ToString())}";
            }

            string getOAuthSecretKey()
            {
                string oAuthSecretkey = "";
                var oAuthSecretKeyHeaders = this.Where(i => i.IsRequiredForSecretKey).ToImmutableList();
                for (int i = 0; i < oAuthSecretKeyHeaders.Count(); ++i)
                {
                    oAuthSecretkey += string.Format("{0}{1}",
                        UrlEncode(oAuthSecretKeyHeaders.ElementAt(i).Value),
                        (i == oAuthSecretKeyHeaders.Count() - 1) ? "" : "&");
                }
                return oAuthSecretkey;
            }
        }

        public IEnumerator<AuthItem> GetEnumerator() => new AuthItem[] { ConsumerKey, ConsumerSecret, BearerToken, BearerTokenSecret }.Where(i => i != null).GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class AuthItem
    {
        public string HeaderName { get; }
        public string Value { get; }
        public bool IsRequiredForSecretKey { get; }
        public bool IsRequiredForSignature { get; }

        public AuthItem(string value, bool isRequiredForSecretKey = false, bool isRequiredForSignature = false, string headerName = null)
        {
            if (string.IsNullOrEmpty(value)) throw new ArgumentNullException(nameof(value));

            Value = value;
            IsRequiredForSecretKey = isRequiredForSecretKey;
            IsRequiredForSignature = isRequiredForSignature;
            HeaderName = headerName;
        }
    }
}