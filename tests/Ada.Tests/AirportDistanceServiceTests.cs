using System;
using System.Threading.Tasks;
using Ada.Api.Exceptions;
using Ada.Api.Models;
using Xunit;

namespace Ada.Tests
{
    public class AirportDistanceServiceTests : TestBase
    {
        [Fact]
        public async Task AirportDistanceService_Should_Return_Correct_Mile_With_0_02_Tolerance()
        {
            // Arrange
            
            // Act
            var mile = await Service.Calculate(new AirportCodes
            {
                From = "AYT",
                To = "IST"
            });

            // Assert
            Assert.True(Math.Abs(mile - 301.27269712793606) <= 0.02);
        }

        [Fact]
        public async Task AirportDistanceService_Should_Return_AirportNotFoundException_When_Passed_Not_Valid_Iata_Airport_Code()
        {
            // Arrange

            // Act
            async Task Act() => await Service.Calculate(new AirportCodes
            {
                From = "",
                To = "IST"
            });

            // Assert
            await Assert.ThrowsAsync<AirportNotFoundException>(Act);
        }
    }
}
