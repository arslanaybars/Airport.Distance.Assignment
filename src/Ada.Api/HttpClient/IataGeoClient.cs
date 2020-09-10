using Microsoft.Extensions.Options;
using System;

namespace Ada.Api.HttpClient
{
    public class IataGeoClient : AdaHttpClient, IIataGeoClient
    {
        public IataGeoClient(IOptions<IataGeoOptions> iataGeoOptions, System.Net.Http.HttpClient httpClient) : base(httpClient)
        {
            httpClient.BaseAddress = new Uri(iataGeoOptions.Value.BaseUrl);
        }
    }
}
