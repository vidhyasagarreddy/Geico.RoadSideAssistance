using Geico.RoadSideAssistanceService.Models;
using GeoCoordinatePortable;

namespace Geico.RoadSideAssistanceService.Services.Extensions
{
    public static class GeoLocationExtensions
    {
        public static double DistanceTo(this Geolocation sourceGeoLocation, Geolocation targetGeoLocation)
        {
            return new GeoCoordinate(sourceGeoLocation.Latitude, sourceGeoLocation.Longitude).GetDistanceTo(new GeoCoordinate(targetGeoLocation.Latitude, targetGeoLocation.Longitude));
        }
    }
}
