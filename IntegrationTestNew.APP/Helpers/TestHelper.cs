using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;

namespace IntegrationTestNew.APP.Helpers
{
    public static class TestHelper
    {
        private static WebApplicationFactory<Program> _webAppFactory;
        private static HttpClient _httpClient;

        // Initializes the WebApplicationFactory and HttpClient
        public static HttpClient GetHttpClient()
        {
            if (_httpClient == null)
            {
                _webAppFactory = new WebApplicationFactory<Program>();
                _httpClient = _webAppFactory.CreateDefaultClient();
            }
            return _httpClient;
        }

        // Configures the base URL for API requests
        public static string GetApiBaseUrl(string endpoint)
        {
            // You can modify the base URL depending on your environment (localhost, production, etc.)
            var baseUrl = "https://localhost:7083/";
            return $"{baseUrl}{endpoint}";
        }
    }
}
