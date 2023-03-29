namespace Geico.RoadSideAssistanceService.Models.Exceptions
{
    public class AssistantNotYetBookedException : Exception
    {
        public AssistantNotYetBookedException() : base("Assistant yet to be booked.!")
        {
        }
    }
}
