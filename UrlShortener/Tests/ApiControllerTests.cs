using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net;
using UrlShortener.Infrastructure;
using Xunit;

namespace UrlShortener.Tests
{

    public class ApiControllerTests
    {
        private readonly HttpClient httpClient;

        public ApiControllerTests()
        {
            var webApplicationFactory = new CustomWebApplicationFactory<Program>();
            httpClient = webApplicationFactory.CreateDefaultClient();
        }

        [Theory]
        [InlineData("", HttpStatusCode.BadRequest)]
        [InlineData(" ", HttpStatusCode.BadRequest)]
        [InlineData("abc", HttpStatusCode.BadRequest)]
        [InlineData("httpww.google.com", HttpStatusCode.BadRequest)]
        [InlineData("http://very.long.io", HttpStatusCode.OK)]
        [InlineData("https://very.long.io", HttpStatusCode.OK)]
        public async Task ShortenUrlTests(string url, HttpStatusCode expectedStatusCode)
        {
            var response = await httpClient.PostAsync("/", new StringContent($"\"{url}\"", System.Text.Encoding.UTF8, "application/json"));
            Assert.Equal(expectedStatusCode, response.StatusCode);
        }

        [Fact]
        public async Task ShouldReturnNotFoundWhenUrlDoesNotExist()
        {
            var response = await httpClient.GetAsync("/abc");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task RedirectShouldWork()
        {
            const string longUrl = "https://very.long.url";
            var response = await httpClient.PostAsync("/", new StringContent($"\"{longUrl}\"", System.Text.Encoding.UTF8, "application/json"));
            var responseContentAsString = await response.Content.ReadAsStringAsync();
            var shortUrl = JsonConvert.DeserializeObject<string>(responseContentAsString);

            response = await httpClient.GetAsync($"{new Uri(shortUrl).PathAndQuery}");
            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
            Assert.Equal(new Uri(longUrl), response.Headers.Location);
        }
    }

    public class CustomWebApplicationFactory<TProgram>: WebApplicationFactory<TProgram> where TProgram : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseContentRoot(".");
            builder.UseEnvironment("Development");

            builder.ConfigureServices(services =>
            {
                services.AddSingleton<IShortUrlStorage, InMemoryStorage>();
                services.AddSingleton<INumberSequence, InMemorySequence>();
            });

            base.ConfigureWebHost(builder);
        }
    }
}
