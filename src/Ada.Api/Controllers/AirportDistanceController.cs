using Ada.Api.Models;
using Ada.Api.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Ada.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AirportDistanceController : ControllerBase
    {
        private readonly IAirportDistanceService _airportDistanceService;

        public AirportDistanceController(IAirportDistanceService airportDistanceService)
        {
            _airportDistanceService = airportDistanceService;
        }

        [HttpGet("Calculate")]
        public async Task<IActionResult> Calculate([FromQuery] AirportCodes airportCodes)
        {
            return Ok(await _airportDistanceService.Calculate(airportCodes));
        }
    }
}
