using Integration.WebAppFactory;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WireMock.Logging;
using WireMock.Server;
using WireMock.Settings;
using Xunit;

namespace Integration
{
    public class BaseTestController : IClassFixture<WebApplicationFactory>
    {
        protected readonly HttpClient HttpClient;
        protected readonly WireMockServer httpMockServer;

        protected async Task<HttpResponseMessage> PostAsync(string url, object request)
            => await HttpClient.PostAsync(url, new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json"));

        protected async Task<HttpResponseMessage> GetAsync(string url)
            => await HttpClient.GetAsync(url);

        protected BaseTestController(WebApplicationFactory factory)
        {
            HttpClient = factory.CreateClient();
            var wiereMockConfig = new WireMockServerSettings
            {
                Urls = new[] { "http://localhost:9093" },
                ReadStaticMappings = true
            };
            httpMockServer = WireMockServer.Start(wiereMockConfig);
            httpMockServer.ResetMappings();
            httpMockServer.ReadStaticMappings("Integration/Mappings/");
        }
    }
}
