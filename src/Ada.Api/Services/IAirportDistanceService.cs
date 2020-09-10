using Ada.Api.Models;
using System.Threading.Tasks;

namespace Ada.Api.Services
{
    public interface IAirportDistanceService
    {
        Task<double> Calculate(AirportCodes airportCodes);
    }
}
