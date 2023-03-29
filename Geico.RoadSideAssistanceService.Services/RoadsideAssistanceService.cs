using Geico.RoadSideAssistanceService.Contracts;
using Geico.RoadSideAssistanceService.Models;
using Geico.RoadSideAssistanceService.Models.Exceptions;
using Geico.RoadSideAssistanceService.Services.Extensions;
using Microsoft.CodeAnalysis;

namespace Geico.RoadSideAssistanceService.Services
{
    public class RoadsideAssistanceService : IRoadsideAssistanceService
    {
        // Using In-Memory storage for assistants
        private List<Assistant> assistants;

        // Using In-Memory storage for assisted service history
        private List<AssistedService> assistedServices;

        public RoadsideAssistanceService()
        {
            // using mock data, so User doesn't end up with no search results by default
            this.assistants = new List<Assistant>()
            {
                new Assistant { Name = "Vidhya", GeoLocation = new Geolocation(33.3165196, -97.0176633), IsAvailable = true },
                new Assistant { Name = "Sagar", GeoLocation = new Geolocation(32.8205862, -96.8719802), IsAvailable = true },
                new Assistant { Name = "Reddy", GeoLocation = new Geolocation(34.0201598, -118.6926046), IsAvailable = true },
                new Assistant { Name = "Matta", GeoLocation = new Geolocation(38.8937335, -77.0847875), IsAvailable = true }
            };

            this.assistedServices = new List<AssistedService>();
        }

        public void AddAssistant(Assistant assistant)
        {
            this.assistants.Add(assistant);
        }

        public SortedSet<Assistant> FindNearestAssistants(Geolocation geolocation, int limit)
        {
            SortedSet<Assistant> sortedAssistants = new SortedSet<Assistant>(new AssistantComparer());

            this.assistants.Where(m => m.IsAvailable).Select(m => new
            {
                TotalDistance = geolocation.DistanceTo(m.GeoLocation),  // Extension method that determines the distance
                Assistant = m
            })
                .OrderBy(m => m.TotalDistance)  // Order by nearest distance
                .Select(m => m.Assistant).Take(limit)   // Select Assistants based on limit
                .ToList().ForEach(m =>
                {
                    sortedAssistants.Add(m);
                });

            return sortedAssistants;
        }

        public void ReleaseAssistant(Customer customer, Assistant assistant)
        {
            var matchingAssistant = this.GetAssistantById(assistant.Id);

            if (matchingAssistant.IsAvailable)
            {
                throw new AssistantNotYetBookedException();
            }

            // Set Assistant availability to "true", so it can be available for other customers
            matchingAssistant.IsAvailable = true;

            // Update AssistedService to be able to track SLA later.
            var assistedService = this.assistedServices.SingleOrDefault(m => m.Customer.Id == customer.Id && m.Assistant.Id == assistant.Id && !m.ReleasedOn.HasValue);
            assistedService.ReleasedOn = DateTime.UtcNow;
        }

        public Optional<Assistant> ReserveAssistant(Customer customer, Geolocation customerLocation)
        {
            // Find nearby Assistant for the customer
            var nearbyAssistant = this.FindNearestAssistants(customerLocation, int.MaxValue).FirstOrDefault();

            // Assistant should exist and be FREE(Available)
            if (nearbyAssistant is null)
            {
                return default;
            }

            // Reserve the assistant, so it is not visible until free
            nearbyAssistant.IsAvailable = false;

            // Add this reservation to AssistedServices
            this.assistedServices.Add(new AssistedService
            {
                Id = Guid.NewGuid(),
                RequestedOn = DateTime.UtcNow,
                Assistant = nearbyAssistant,
                Customer = customer
            });

            return new Optional<Assistant>(nearbyAssistant);
        }

        // Reserve based on nearby assistant selected by User
        public Optional<Assistant> ReserveAssistant(Customer customer, Guid assistantId)
        {
            // Find Assistant selected by User
            var nearbyAssistant = this.GetAssistantById(assistantId);

            // Assistant should exist and be FREE(Available)
            if (nearbyAssistant is null || (nearbyAssistant is not null && !nearbyAssistant.IsAvailable))
            {
                return default;
            }

            // Reserve the assistant, so it is not visible until free
            nearbyAssistant.IsAvailable = false;

            // Add this reservation to AssistedServices
            this.assistedServices.Add(new AssistedService
            {
                Id = Guid.NewGuid(),
                RequestedOn = DateTime.UtcNow,
                Assistant = nearbyAssistant,
                Customer = customer
            });

            return new Optional<Assistant>(nearbyAssistant);
        }

        public void UpdateAssistantLocation(Assistant assistant, Geolocation assistantLocation)
        {
            var matchingAssistant = this.GetAssistantById(assistant.Id);
            matchingAssistant.GeoLocation = assistantLocation;
        }

        private Assistant GetAssistantById(Guid id)
        {
            var assistant = this.assistants.SingleOrDefault(m => m.Id == id);
            if (assistant is null)
            {
                throw new AssistantNotFoundException();
            }

            return assistant;
        }
    }
}
