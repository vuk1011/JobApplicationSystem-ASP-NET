using Domain.Entities;
using Domain.Repositories;
using FluentValidation;
using JobApplicationAPI.DTOs;
using JobApplicationAPI.DTOs.JobApplications;
using JobApplicationAPI.DTOs.JobPostings;
using JobApplicationAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobApplicationAPI.Controllers.Candidates
{
    [ApiController]
    [Route("api/candidates/job-applications")]
    [Authorize(Roles = "Candidate")]
    public class JobApplicationsController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        private readonly IValidator<SubmitJobApplicationRequest> _submitValidator;

        private readonly CurrentUserService _currentUser;

        public JobApplicationsController(IUnitOfWork uow, IValidator<SubmitJobApplicationRequest> submitValidator, CurrentUserService currentUser)
        {
            _uow = uow;
            _submitValidator = submitValidator;
            _currentUser = currentUser;
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse>> Submit([FromBody] SubmitJobApplicationRequest request)
        {
            var validationResult = await _submitValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(new ApiResponse(string.Join(" ", validationResult.Errors.Select(e => e.ErrorMessage))));
            }

            var candidate = await _currentUser.GetCurrentAsync<Candidate>();
            if (candidate is null)
            {
                return Unauthorized(new ApiResponse("Candidate not found"));
            }

            var jobPosting = _uow.JobPostings.GetByIdWithCompany(request.JobPostingId);
            if (jobPosting is null)
            {
                return NotFound(new ApiResponse("Job posting not found"));
            }

            if (_uow.JobApplications.existsByCandidateIdAndJobPostingId(candidate.Id, request.JobPostingId))
            {
                return Conflict(new ApiResponse("You already applied for this job posting"));
            }

            if (jobPosting.IsClosed)
            {
                return BadRequest(new ApiResponse("Job posting closed"));
            }

            var application = new JobApplication
            {
                DateOfSubmission = DateOnly.FromDateTime(DateTime.Today),
                Status = JobApplicationStatus.Submitted,
                JobPostingId = jobPosting.Id,
                CandidateId = candidate.Id,
            };
            _uow.JobApplications.Add(application);
            await _uow.SaveChangesAsync();

            return Ok(new ApiResponse("Successfully submitted an application"));
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<JobApplicationCandidateDto>>>> GetAll()
        {
            var candidate = await _currentUser.GetCurrentAsync<Candidate>();
            if (candidate is null)
            {
                return Unauthorized(new ApiResponse("Candidate not found"));
            }

            var applications = _uow.JobApplications.GetByCandidateId(candidate.Id).Select(ToDto).ToList();
            return Ok(new ApiResponse<List<JobApplicationCandidateDto>>("Successfully retrieved all applications", applications));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<JobApplicationCandidateDto>>> Get([FromRoute] long id)
        {
            var candidate = await _currentUser.GetCurrentAsync<Candidate>();
            if (candidate is null)
            {
                return Unauthorized(new ApiResponse("Candidate not found"));
            }

            var application = _uow.JobApplications.GetByIdForCandidate(id, candidate.Id);
            if (application is null)
            {
                return NotFound(new ApiResponse("Job application not found"));
            }

            return Ok(new ApiResponse<JobApplicationCandidateDto>("Successfully retrieved the application", ToDto(application)));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse>> Withdraw([FromRoute] long id)
        {
            var candidate = await _currentUser.GetCurrentAsync<Candidate>();
            if (candidate is null)
            {
                return Unauthorized(new ApiResponse("Candidate not found"));
            }

            var application = _uow.JobApplications.GetByIdForCandidate(id, candidate.Id);
            if (application is null)
            {
                return NotFound(new ApiResponse("Job application not found"));
            }

            if (application.Status is JobApplicationStatus.Offered or JobApplicationStatus.Accepted or JobApplicationStatus.Rejected)
            {
                return Conflict(new ApiResponse("Job application cannot be withdrawn if state is Offered, Accepted or Rejected"));
            }

            _uow.JobApplications.Remove(application);
            await _uow.SaveChangesAsync();

            return Ok(new ApiResponse("Successfully withdrawn an application"));
        }

        private static JobApplicationCandidateDto ToDto(JobApplication application) => new()
        {
            Id = application.Id,
            DateOfSubmission = application.DateOfSubmission,
            Status = application.Status,
            JobPosting = new JobPostingDto
            {
                Id = application.JobPosting.Id,
                Title = application.JobPosting.Title,
                Description = application.JobPosting.Description,
                DateOfPublishing = application.JobPosting.DateOfPublishing,
                DateOfExpiration = application.JobPosting.DateOfExpiration,
                Status = application.JobPosting.Status,
                IsClosed = application.JobPosting.IsClosed,
                CompanyName = application.JobPosting.Company?.Name ?? string.Empty,
            },
        };
    }
}
