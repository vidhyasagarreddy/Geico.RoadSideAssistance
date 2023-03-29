using Geico.RoadSideAssistanceService.API.ViewModel;
using Geico.RoadSideAssistanceService.Contracts;
using Geico.RoadSideAssistanceService.Models;
using Geico.RoadSideAssistanceService.Models.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Geico.RoadSideAssistanceService.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoadsideAssistanceServiceController : ControllerBase
    {
        private readonly IRoadsideAssistanceService roadsideAssistanceService;

        public RoadsideAssistanceServiceController(IRoadsideAssistanceService roadsideAssistanceService)
        {
            this.roadsideAssistanceService = roadsideAssistanceService;
        }

        [HttpPost("assistants")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult AddAssistant([FromBody] Assistant assistant)
        {
            this.roadsideAssistanceService.AddAssistant(assistant);
            return this.Ok();
        }

        [HttpGet("assistants/nearby")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<SortedSet<Assistant>> FindNearestAssistants(
            [FromQuery, Required] double latitude,
            [FromQuery, Required] double longitude,
            [FromQuery] int limit = 5)
        {
            var nearestAssistants = this.roadsideAssistanceService.FindNearestAssistants(new Geolocation(latitude, longitude), limit);
            return this.Ok(nearestAssistants);
        }

        [HttpPost("assistants/reserve/nearby")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<Optional<Assistant>> ReserveAssistant([FromBody] ReserveAssistantViewModel reserveAssistantViewModel)
        {
            var assistant = this.roadsideAssistanceService.ReserveAssistant(reserveAssistantViewModel.Customer, reserveAssistantViewModel.CustomerLocation);
            return this.Ok(assistant);
        }

        [HttpPost("assistants/{id}/reserve")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Optional<Assistant>> ReserveAssistantById([FromBody] Customer customer, Guid id)
        {
            try
            {
                var assistant = this.roadsideAssistanceService.ReserveAssistant(customer, id);
                return this.Ok(assistant);
            }
            catch (AssistantNotFoundException ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

        [HttpPatch("assistants/release")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult ReleaseAssistant([FromBody] ReleaseAssistantViewModel releaseAssistantViewModel)
        {
            try
            {
                this.roadsideAssistanceService.ReleaseAssistant(releaseAssistantViewModel.Customer, releaseAssistantViewModel.Assistant);
                return this.Ok();
            }
            catch (AssistantNotFoundException ex)
            {
                return this.BadRequest(ex.Message);
            }
            catch(AssistantNotYetBookedException ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

        [HttpPatch("assistants/location")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateAssistantLocation([FromBody] UpdateAssistantLocationViewModel updateAssistantLocationViewModel)
        {
            try
            {
                this.roadsideAssistanceService.UpdateAssistantLocation(updateAssistantLocationViewModel.Assistant, updateAssistantLocationViewModel.GeoLocation);
                return this.Ok();
            }
            catch (AssistantNotFoundException ex)
            {
                return this.BadRequest(ex.Message);
            }
        }
    }
}