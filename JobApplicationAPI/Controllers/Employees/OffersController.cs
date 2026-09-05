using JobApplicationAPI.Commands.Offers;
using JobApplicationAPI.DTOs;
using JobApplicationAPI.DTOs.Offers;
using JobApplicationAPI.Queries.Offers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JobApplicationAPI.Controllers.Employees
{
    [ApiController]
    [Route("api/employees/offers")]
    [Authorize(Roles = "Employee")]
    public class OffersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OffersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<OfferDto>>>> GetAll([FromQuery] long jobApplicationId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var offers = await _mediator.Send(new GetOffersByJobApplicationForEmployeeQuery(userId, jobApplicationId));

            return Ok(new ApiResponse<List<OfferDto>>("Offers retrieved", offers));
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse>> Create([FromBody] CreateOfferRequest request)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            await _mediator.Send(new CreateOfferCommand(userId, request));

            return Ok(new ApiResponse("Offer created"));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse>> Delete([FromRoute] long id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            await _mediator.Send(new DeleteOfferCommand(userId, id));

            return Ok(new ApiResponse("Offer deleted"));
        }
    }
}
