using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Reddit2Kindle.Contracts;

namespace Reddit2Kindle.Blazor.Services
{
    public class Reddit2KindleService
    {
        private readonly string _baseUri;
        private readonly HttpClient _client;

        public Reddit2KindleService(HttpClient client, IHostEnvironment environment)
        {
            _client = client;
            _baseUri = environment.IsDevelopment()
                ? "http://localhost:7071"
                : "https://reddit2kindle-functions.azurewebsites.net";
        }

        public async Task SubmitPost(PostRequest postRequest)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"{_baseUri}/api/Post"),
                Content = new StringContent(JsonSerializer.Serialize(postRequest), Encoding.UTF8, "application/json")
            };
            var response = await _client.SendAsync(request);
        }
    }
}
