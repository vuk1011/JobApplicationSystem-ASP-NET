using JobApplicationAPI.Commands.Offers;
using JobApplicationAPI.DTOs;
using JobApplicationAPI.DTOs.Offers;
using JobApplicationAPI.Queries.Offers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JobApplicationAPI.Controllers.Candidates
{
    [ApiController]
    [Route("api/candidates/offers")]
    [Authorize(Roles = "Candidate")]
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

            var offers = await _mediator.Send(new GetOffersByJobApplicationForCandidateQuery(userId, jobApplicationId));

            return Ok(new ApiResponse<List<OfferDto>>("Successfully retrieved offers for job application", offers));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse>> Update([FromRoute] long id, [FromBody] UpdateOfferRequest request)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            await _mediator.Send(new UpdateOfferCommand(userId, id, request));

            return Ok(new ApiResponse("Successfully updated offer"));
        }
    }
}
