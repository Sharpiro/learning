using Twitterizer;

namespace TwitterApi
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //const string key = "3177206249-DrjPnmdizWmv0GcexcxrJ6kh7ZrVjWpUclxfeHs";
            //var bytes = Encoding.UTF8.GetBytes(key);
            //var base64String = Convert.ToBase64String(bytes);

            var tokens = new OAuthTokens()
            {
                ConsumerKey = "GhCFnSgWZfetVo8pXhBFLAHfi",
                ConsumerSecret = "d6pmneC2EdxgG0S7Zhxbj98xxzKGhJLmHgVZutknZTLwKpJwyj",
                AccessToken = "3177206249-DrjPnmdizWmv0GcexcxrJ6kh7ZrVjWpUclxfeHs",
                AccessTokenSecret = "rlpq483Qk4R9sXJ9IpOti0PdMuPKsFjqna9cRSwk6B9RY"
            };
            //TwitterStatusCollection homeTimeline = TwitterStatus.GetHomeTimeline(tokens);
            var isValid = TwitterAccount.VerifyCredentials(tokens);
        }
    }
}