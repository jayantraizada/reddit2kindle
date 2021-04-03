using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Reddit2Kindle.Contracts;
using static Reddit2Kindle.Functions.Constants;

namespace Reddit2Kindle.Functions.Functions
{
    public class HttpFunction
    {
        private readonly ILogger<HttpFunction> _logger;

        public HttpFunction(ILogger<HttpFunction> logger)
        {
            _logger = logger;
        }

        [Function("Post")]
        public async Task<HttpAndQueuePost> PostAsync([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)]
            HttpRequestData req, FunctionContext context)
        {
            var postRequest = await req.ReadFromJsonAsync<PostRequest>();
            return new HttpAndQueuePost
            {
                PostRequest = postRequest,
                HttpResponseData = req.CreateResponse(HttpStatusCode.Accepted)
            };
        }

        [Function("Subreddit")]
        public async Task<HttpAndQueueSubreddit> SubredditAsync([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)]
            HttpRequestData req, FunctionContext context)
        {
            var subredditRequest = await req.ReadFromJsonAsync<SubredditRequest>();
            return new HttpAndQueueSubreddit
            {
                SubredditRequest = subredditRequest,
                HttpResponseData = req.CreateResponse(HttpStatusCode.Accepted)
            };
        }

        public class HttpAndQueuePost
        {
            [QueueOutput(PostQueue)]
            public PostRequest PostRequest { get; init; }

            public HttpResponseData HttpResponseData { get; init; }
        }

        public class HttpAndQueueSubreddit
        {
            [QueueOutput(SubredditQueue)]
            public SubredditRequest SubredditRequest { get; init; }

            public HttpResponseData HttpResponseData { get; init; }
        }
    }
}
