using System.Collections.Generic;

namespace TwitterApi.Core.Models
{
    public class Tweet
    {
        public long Id { get; set; }
        public string Text { get; set; }
        public IEnumerable<string> Hashtags { get; set; }
        public IEnumerable<User> UserMentions { get; set; }
        public IEnumerable<string> Urls { get; set; }
        public User User { get; set; }
    }
}