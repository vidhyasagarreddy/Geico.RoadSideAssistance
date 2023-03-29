using Geico.RoadSideAssistanceService.Models;
using System.ComponentModel.DataAnnotations;

namespace Geico.RoadSideAssistanceService.API.ViewModel
{
    public class UpdateAssistantLocationViewModel
    {
        [Required]
        public Assistant Assistant { get; set; }

        [Required]
        public Geolocation GeoLocation { get; set; }
    }
}
