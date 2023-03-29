namespace Geico.RoadSideAssistanceService.Models
{
    public class AssistedService
    {
        public Guid Id { get; set; }

        public Customer Customer { get; set; }

        public Assistant Assistant { get; set; }

        public DateTime RequestedOn { get; set; }

        public DateTime? ReleasedOn { get; set; }

        // To Track SLA
        public double TotalServiceTimeInHours
        {
            get
            {
                return this.ReleasedOn.HasValue ? this.ReleasedOn.Value.Subtract(this.RequestedOn).TotalHours : default;
            }
        }
    }
}
