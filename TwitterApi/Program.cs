using TwitterApi.Core;

namespace TwitterApi
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            const string consumerKey = "GhCFnSgWZfetVo8pXhBFLAHfi";
            const string consumerSecret = "d6pmneC2EdxgG0S7Zhxbj98xxzKGhJLmHgVZutknZTLwKpJwyj";
            const string bearerToken = "3177206249-DrjPnmdizWmv0GcexcxrJ6kh7ZrVjWpUclxfeHs";
            const string bearerTokenSecret = "rlpq483Qk4R9sXJ9IpOti0PdMuPKsFjqna9cRSwk6B9RY";
            var authItems = new AuthItems(consumerKey, consumerSecret, bearerToken, bearerTokenSecret);
            var apiHelper = TwitterApiService.CreateSingleUserService(authItems);
            var data = apiHelper.GetTimeLine().Result;
        }
    }
}