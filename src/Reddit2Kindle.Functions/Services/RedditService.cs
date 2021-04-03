using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Reddit;
using Reddit.Controllers;
using Reddit2Kindle.Functions.Contracts.Options;
using Reddit2Kindle.Functions.Utils;

namespace Reddit2Kindle.Functions.Services
{
    public class RedditService
    {
        private readonly ILogger<RedditService> _logger;
        private readonly RedditClient _redditClient;

        public RedditService(ILogger<RedditService> logger, IOptions<RedditOptions> redditOptions)
        {
            _logger = logger;
            var options = redditOptions.Value;
            _redditClient = new RedditClient(options.AppId, appSecret: options.AppSecret, refreshToken: options.RefreshToken);
        }

        public Post GetPost(string uri)
        {
            var id = RedditUtils.ExtractPostId(uri);
            return _redditClient.Post($"t3_{id}").About();
        }

        public IEnumerable<Post> GetSubredditPosts(string subreddit)
        {
            return _redditClient.Subreddit(subreddit).Posts.GetTop(limit: 25);
        }
    }
}
