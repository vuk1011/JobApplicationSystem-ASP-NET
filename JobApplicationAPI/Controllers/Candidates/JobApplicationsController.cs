using JobApplicationAPI.Commands.JobApplications;
using JobApplicationAPI.DTOs;
using JobApplicationAPI.DTOs.JobApplications;
using JobApplicationAPI.Queries.JobApplications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JobApplicationAPI.Controllers.Candidates
{
    [ApiController]
    [Route("api/candidates/job-applications")]
    [Authorize(Roles = "Candidate")]
    public class JobApplicationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public JobApplicationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse>> Submit([FromBody] SubmitJobApplicationRequest request)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            await _mediator.Send(new CreateJobApplicationCommand(userId, request));

            return Ok(new ApiResponse("Successfully submitted an application"));
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<JobApplicationCandidateDto>>>> GetAll()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var jobApplications = await _mediator.Send(new GetJobApplicationsForCandidateQuery(userId));

            return Ok(new ApiResponse<List<JobApplicationCandidateDto>>("Successfully retrieved all applications", jobApplications));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<JobApplicationCandidateDto>>> Get([FromRoute] long id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var jobApplication = await _mediator.Send(new GetJobApplicationForCandidateQuery(userId, id));

            return Ok(new ApiResponse<JobApplicationCandidateDto>("Successfully retrieved the application", jobApplication));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse>> Withdraw([FromRoute] long id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            await _mediator.Send(new DeleteJobApplicationCommand(userId, id));

            return Ok(new ApiResponse("Successfully withdrawn an application"));
        }
    }
}
