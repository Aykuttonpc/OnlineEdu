using System.Net.Http.Headers;

namespace OnlineEdu.WebUI.Helpers
{
    public static class HttpClientInstance
    {
        public static HttpClient CreateClient()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7071/api/");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer");
            return client;
        }
    }
}
