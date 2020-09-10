using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using Ada.Api.HttpClient;
using Ada.Api.HttpClient.Models;
using Ada.Api.Services;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Contrib.HttpClient;

namespace Ada.Tests
{
    public class TestBase
    {
        public HttpClient FakeHttpClient { get; set; }
        public IIataGeoClient FakeIataGeoClient { get; set; }

        protected readonly AirportDistanceService Service;

        public TestBase()
        {
            CreateIataGeoFakeClient();
            Service = new AirportDistanceService(FakeIataGeoClient);
        }

        private void CreateIataGeoFakeClient()
        {
            var handler = new Mock<HttpMessageHandler>();
            FakeHttpClient = handler.CreateClient();

            handler.SetupAnyRequest()
                .ReturnsResponse(HttpStatusCode.NotFound);

            handler.SetupRequest("http://iatageo.com/getLatLng/AYT")
                .Returns(async (HttpRequestMessage request, CancellationToken _) => new HttpResponseMessage()
                {
                    Content = new StringContent(JsonSerializer.Serialize(new AirportResponse
                    {
                        Latitude = "36.898701",
                        Longitude = "30.800501",
                        Code = "AYT",
                        Name = "Antalya International Airport"
                    }))
                });

            handler.SetupRequest("http://iatageo.com/getLatLng/IST")
                .Returns(async (HttpRequestMessage request, CancellationToken _) => new HttpResponseMessage()
                {
                    Content = new StringContent(JsonSerializer.Serialize(new AirportResponse
                    {
                        Latitude = "40.9768981934",
                        Longitude = "28.8145999908",
                        Code = "IST",
                        Name = "Atatürk International Airport"
                    }))
                });

            FakeIataGeoClient = new IataGeoClient(Options.Create(new IataGeoOptions()
            {
                BaseUrl = "http://iatageo.com"
            }), FakeHttpClient);
        }

    }
}
