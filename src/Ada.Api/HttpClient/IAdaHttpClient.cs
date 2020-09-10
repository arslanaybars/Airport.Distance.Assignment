using System.Threading.Tasks;

namespace Ada.Api.HttpClient
{
    public interface IAdaHttpClient
    {
        HttpActionResponse<T> Get<T>(string endpoint, string parameters = null) where T : class;

        Task<HttpActionResponse<T>> GetAsync<T>(string endpoint, string parameters = null) where T : class;
    }
}
