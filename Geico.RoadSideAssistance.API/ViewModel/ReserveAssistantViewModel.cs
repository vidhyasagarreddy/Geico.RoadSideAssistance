using Geico.RoadSideAssistanceService.Models;
using System.ComponentModel.DataAnnotations;

namespace Geico.RoadSideAssistanceService.API.ViewModel
{
    public class ReserveAssistantViewModel
    {
        [Required]
        public Customer Customer { get; set; }

        [Required]
        public Geolocation CustomerLocation { get; set; }
    }
}
