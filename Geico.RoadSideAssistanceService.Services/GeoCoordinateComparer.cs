using Geico.RoadSideAssistanceService.Models;

namespace Geico.RoadSideAssistanceService.Services
{
    internal class GeoCoordinateComparer : IComparer<Assistant>
    {
        public int Compare(Assistant x, Assistant y)
        {
            throw new NotImplementedException();
        }

        public int Compare((Assistant, Geolocation) x, (Assistant, Geolocation) y)
        {
            throw new NotImplementedException();
        }
    }
}
