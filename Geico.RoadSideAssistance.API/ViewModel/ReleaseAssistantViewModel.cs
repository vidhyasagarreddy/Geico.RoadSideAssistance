using Geico.RoadSideAssistanceService.Models;
using System.ComponentModel.DataAnnotations;

namespace Geico.RoadSideAssistanceService.API.ViewModel
{
    public class ReleaseAssistantViewModel
    {
        [Required]
        public Customer Customer { get; set; }

        [Required]
        public Assistant Assistant { get; set; }
    }
}
