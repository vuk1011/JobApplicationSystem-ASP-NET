using JobApplicationAPI.Commands.JobApplications;
using JobApplicationAPI.DTOs;
using JobApplicationAPI.DTOs.JobApplications;
using JobApplicationAPI.DTOs.Users;
using JobApplicationAPI.Queries.JobApplications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JobApplicationAPI.Controllers.Employees
{
    [ApiController]
    [Route("api/employees/job-applications")]
    [Authorize(Roles = "Employee")]
    public class JobApplicationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public JobApplicationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("job-posting/{jobPostingId}")]
        public async Task<ActionResult<ApiResponse<List<JobApplicationEmployeeDto>>>> GetAllByJobPosting([FromRoute] long jobPostingId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var applications = await _mediator.Send(new GetUnmanagedJobApplicationsByJobPostingQuery(userId, jobPostingId));

            return Ok(new ApiResponse<List<JobApplicationEmployeeDto>>("Job applications retrieved", applications));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<JobApplicationEmployeeDto>>> Get([FromRoute] long id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var application = await _mediator.Send(new GetUnmanagedJobApplicationQuery(userId, id));

            return Ok(new ApiResponse<JobApplicationEmployeeDto>("Job application retrieved", application));
        }

        [HttpGet("managed")]
        public async Task<ActionResult<ApiResponse<List<JobApplicationEmployeeDto>>>> GetAllManaged()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var applications = await _mediator.Send(new GetManagedJobApplicationsQuery(userId));

            return Ok(new ApiResponse<List<JobApplicationEmployeeDto>>("Managed job applications retrieved", applications));
        }

        [HttpPut("managed")]
        public async Task<ActionResult<ApiResponse>> AddToManaged([FromBody] ManageJobApplicationRequest request)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            await _mediator.Send(new UpdateJobApplicationToManagedCommand(userId, request));

            return Ok(new ApiResponse("Job application added to managed list"));
        }

        [HttpGet("managed/{id}")]
        public async Task<ActionResult<ApiResponse<JobApplicationEmployeeDto>>> GetManaged([FromRoute] long id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var application = await _mediator.Send(new GetManagedJobApplicationQuery(userId, id));

            return Ok(new ApiResponse<JobApplicationEmployeeDto>("Job application retrieved", application));
        }

        [HttpPut("managed/{id}")]
        public async Task<ActionResult<ApiResponse>> UpdateStatus([FromRoute] long id, [FromBody] UpdateJobApplicationStatusRequest request)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            await _mediator.Send(new UpdateJobApplicationStatusCommand(userId, id, request));

            return Ok(new ApiResponse("Job application status updated"));
        }

        [HttpGet("managed/{id}/candidate/profile")]
        public async Task<ActionResult<ApiResponse<CandidateDto>>> GetCandidateProfileForJobApplication([FromRoute] long id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var candidate = await _mediator.Send(new GetCandidateForJobApplicationQuery(userId, id));

            return Ok(new ApiResponse<CandidateDto>("Candidate profile retrieved", candidate));
        }

        [HttpGet("managed/{id}/candidate/resume")]
        public async Task<IActionResult> GetCandidateResumeForJobApplication([FromRoute] long id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var resumeBytes = await _mediator.Send(new GetCandidateResumeForManagedJobApplicationQuery(userId, id));

            Response.Headers.ContentDisposition = "inline; filename=\"resume.pdf\"";
            return File(resumeBytes, "application/pdf");
        }
    }
}
