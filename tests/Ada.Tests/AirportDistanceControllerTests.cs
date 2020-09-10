using System;
using System.Threading.Tasks;
using Ada.Api.Controllers;
using Ada.Api.Exceptions;
using Ada.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Ada.Tests
{
    public class AirportDistanceControllerTests : TestBase
    {
        private readonly AirportDistanceController _controller;

        public AirportDistanceControllerTests()
        {
            _controller = new AirportDistanceController(Service);
        }

        [Fact]
        public async Task AirportDistanceController_Calculate_Should_Return_Ok_With_Mile()
        {
            // Arrange

            // Act
            var result = await _controller.Calculate(new AirportCodes
            {
                From = "AYT",
                To = "IST"
            });

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var mileResult = result as OkObjectResult;
            Assert.True(Math.Abs(Convert.ToDouble(mileResult?.Value) - 301.27269712793606) <= 0.02);
        }

        [Fact]
        public async Task AirportDistanceController_Calculate_Should_Return_AirportNotFoundException_When_Passed_Not_Valid_Iata_Airport_Code()
        {
            // Arrange

            // Act
            async Task Act() => await _controller.Calculate(new AirportCodes
            {
                From = "",
                To = "IST"
            });

            // Assert
            await Assert.ThrowsAsync<AirportNotFoundException>(Act);
        }
    }
}
