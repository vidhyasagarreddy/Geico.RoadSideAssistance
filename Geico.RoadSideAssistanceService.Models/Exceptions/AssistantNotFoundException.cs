namespace Geico.RoadSideAssistanceService.Models.Exceptions
{
    public class AssistantNotFoundException : Exception
    {
        public AssistantNotFoundException() : base("No Matching Assistant found !")
        {
        }
    }
}
