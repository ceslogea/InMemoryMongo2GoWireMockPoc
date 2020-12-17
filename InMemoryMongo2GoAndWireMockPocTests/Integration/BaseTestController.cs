using InMemoryMongo2GoAndWireMockPocTests.Integration.WebAppFactory;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace InMemoryMongo2GoAndWireMockPocTests.Integration
{
    public class BaseTestController : IClassFixture<WebApplicationFactory>
    {
        protected readonly HttpClient HttpClient;

        protected async Task<HttpResponseMessage> Post(string url, object request)
            => await HttpClient.PostAsync(url, new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json"));

        protected async Task<HttpResponseMessage> Get(string url)
            => await HttpClient.GetAsync(url);

        protected BaseTestController(WebApplicationFactory factory) => HttpClient = factory.CreateClient();

    }
}
