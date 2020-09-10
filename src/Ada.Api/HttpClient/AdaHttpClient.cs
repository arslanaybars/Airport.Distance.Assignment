using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Ada.Api.HttpClient
{
    public class AdaHttpClient : IAdaHttpClient
    {
        private readonly System.Net.Http.HttpClient _httpClient;

        public AdaHttpClient(System.Net.Http.HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public HttpActionResponse<T> Get<T>(string endpoint, string parameters = null) where T : class
        {
            return GetAsync<T>(endpoint, parameters).GetAwaiter().GetResult();
        }

        public async Task<HttpActionResponse<T>> GetAsync<T>(string endpoint, string parameters = null) where T : class
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync(endpoint);
            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                return new HttpActionResponse<T>()
                {
                    IsSuccessStatusCode = httpResponseMessage.IsSuccessStatusCode,
                    StatusCode = httpResponseMessage.StatusCode,
                };
            }

            var strContent = await httpResponseMessage.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<T>(strContent);
            return new HttpActionResponse<T>()
            {
                Response = result,
                IsSuccessStatusCode = httpResponseMessage.IsSuccessStatusCode,
                StatusCode = httpResponseMessage.StatusCode
            };
        }
    }
}
