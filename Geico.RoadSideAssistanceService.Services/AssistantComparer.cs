using Geico.RoadSideAssistanceService.Models;
using Geico.RoadSideAssistanceService.Services.Extensions;

namespace Geico.RoadSideAssistanceService.Services
{
    internal class AssistantComparer : IComparer<Assistant>
    {
        public int Compare(Assistant x, Assistant y)
        {
            var distance = x.GeoLocation.DistanceTo(y.GeoLocation);
            return distance < 0 ? -1 : 1;
        }
    }
}
