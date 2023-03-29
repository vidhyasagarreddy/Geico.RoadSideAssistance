namespace Geico.RoadSideAssistanceService.Models
{
    public class Assistant
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; }

        public bool IsAvailable { get; set; }

        public Geolocation GeoLocation { get; set; }
    }
}
