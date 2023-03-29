namespace Geico.RoadSideAssistanceService.Models
{
    public class Geolocation
    {
        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public Geolocation(double latitude, double longitude)
        {
            this.Latitude = latitude;
            this.Longitude = longitude;
        }
    }
}
