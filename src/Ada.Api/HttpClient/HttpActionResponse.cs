using System.Net;

namespace Ada.Api.HttpClient
{
    public class HttpActionResponse<T>
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccessStatusCode { get; set; }
        public string ErrorResponse { get; set; }
        public T Response { get; set; }
    }
}
