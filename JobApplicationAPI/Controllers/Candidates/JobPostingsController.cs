using JobApplicationAPI.DTOs;
using JobApplicationAPI.DTOs.JobPostings;
using JobApplicationAPI.Queries.JobPostings;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JobApplicationAPI.Controllers.Candidates
{
    [ApiController]
    [Route("api/candidates/job-postings")]
    [Authorize(Roles = "Candidate")]
    public class JobPostingsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public JobPostingsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<JobPostingDto>>>> GetAll()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var jobPostings = await _mediator.Send(new GetJobPostingsPublishedQuery(userId));

            return Ok(new ApiResponse<List<JobPostingDto>>("Job postings retrieved", jobPostings));
        }
    }
}
