using JobApplicationAPI.DTOs;
using JobApplicationAPI.DTOs.Interviews;
using JobApplicationAPI.Queries.Interviews;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JobApplicationAPI.Controllers.Candidates
{
    [ApiController]
    [Route("api/candidates/interviews")]
    [Authorize(Roles = "Candidate")]
    public class InterviewsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public InterviewsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<InterviewDto>>>> GetAll([FromQuery] long jobApplicationId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var interviews = await _mediator.Send(new GetInterviewsForCandidateQuery(userId, jobApplicationId));

            return Ok(new ApiResponse<List<InterviewDto>>("Successfully retrieved interviews for job application", interviews));
        }
    }
}
