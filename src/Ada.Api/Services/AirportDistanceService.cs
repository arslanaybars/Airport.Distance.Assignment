using Ada.Api.Exceptions;
using Ada.Api.Extensions;
using Ada.Api.HttpClient;
using Ada.Api.HttpClient.Models;
using Ada.Api.Models;
using System.Threading.Tasks;

namespace Ada.Api.Services
{
    public class AirportDistanceService : IAirportDistanceService
    {
        private readonly IIataGeoClient _iataGeoClient;

        public AirportDistanceService(IIataGeoClient iataGeoClient)
        {
            _iataGeoClient = iataGeoClient;
        }

        public async Task<double> Calculate(AirportCodes airportCodes)
        {
            var from = await _iataGeoClient.GetAsync<AirportResponse>(Endpoints.GetCoordinates + $"/{airportCodes.From}");

            if (!from.IsSuccessStatusCode)
            {
                throw new AirportNotFoundException();
            }

            var to = await _iataGeoClient.GetAsync<AirportResponse>(Endpoints.GetCoordinates + $"/{airportCodes.To}");

            if (!to.IsSuccessStatusCode)
            {
                throw new AirportNotFoundException();
            }

            var airportCoordinates = new AirportCoordinates
            {
                From = new Airport(from.Response),
                To = new Airport(to.Response)
            };

            return airportCoordinates.GetDistanceInMiles();
        }
    }
}
