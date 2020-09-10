using Ada.Api.Models;
using System;

namespace Ada.Api.Extensions
{
    public static class DistanceExtensions
    {
        public static double GetDistanceInMiles(this AirportCoordinates airportCoordinates)
        {
            var rFromLatitude = Math.PI * airportCoordinates.From.Latitude / 180;
            var rToLatitude = Math.PI * airportCoordinates.To.Latitude / 180;
            var theta = airportCoordinates.From.Longitude - airportCoordinates.To.Longitude;
            var rTheta = Math.PI * theta / 180;

            var distInMiles =
                Math.Sin(rFromLatitude) * Math.Sin(rToLatitude) + Math.Cos(rFromLatitude) *
                Math.Cos(rToLatitude) * Math.Cos(rTheta);

            distInMiles = Math.Acos(distInMiles);
            distInMiles = distInMiles * 180 / Math.PI;
            distInMiles = distInMiles * 60 * 1.1515;

            return distInMiles;
        }
    }
}
