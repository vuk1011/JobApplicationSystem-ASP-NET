using Domain.Entities;
using Domain.Repositories;
using FluentValidation;
using Infrastructure.Identity;
using JobApplicationAPI.DTOs;
using JobApplicationAPI.DTOs.JobApplications;
using JobApplicationAPI.DTOs.Users;
using JobApplicationAPI.Services;
using JobApplicationAPI.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JobApplicationAPI.Controllers.Employees
{
    [ApiController]
    [Route("api/employees/job-applications")]
    [Authorize(Roles = "Employee")]
    public class JobApplicationsController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly UserManager<AppUser> _userManager;
        private readonly IValidator<ManageJobApplicationRequest> _manageValidator;
        private readonly IValidator<UpdateJobApplicationStatusRequest> _updateValidator;
        private readonly CurrentUserService _currentUser;

        public JobApplicationsController(
            IUnitOfWork uow,
            UserManager<AppUser> userManager,
            IValidator<ManageJobApplicationRequest> manageValidator,
            IValidator<UpdateJobApplicationStatusRequest> updateValidator,
            CurrentUserService currentUser)
        {
            _uow = uow;
            _userManager = userManager;
            _manageValidator = manageValidator;
            _updateValidator = updateValidator;
            _currentUser = currentUser;
        }

        [HttpGet("job-posting/{jobPostingId}")]
        public ActionResult<ApiResponse<List<JobApplicationEmployeeDto>>> GetAllByJobPosting([FromRoute] long jobPostingId)
        {
            var jobPosting = _uow.JobPostings.GetByIdWithCompany(jobPostingId);
            if (jobPosting is null)
            {
                return NotFound(new ApiResponse("Job posting not found"));
            }

            var applications = _uow.JobApplications.GetUnmanagedByJobPostingId(jobPostingId).Select(JobApplicationMapper.ToEmployeeDto).ToList();
            return Ok(new ApiResponse<List<JobApplicationEmployeeDto>>("Job applications retrieved", applications));
        }

        [HttpGet("{id}")]
        public ActionResult<ApiResponse<JobApplicationEmployeeDto>> Get([FromRoute] long id)
        {
            var application = _uow.JobApplications.GetByIdWithDetails(id);
            if (application is null)
            {
                return NotFound(new ApiResponse("Job application not found"));
            }
            if (application.IsManaged)
            {
                return Conflict(new ApiResponse("This job application is already managed"));
            }

            return Ok(new ApiResponse<JobApplicationEmployeeDto>("Job application retrieved", JobApplicationMapper.ToEmployeeDto(application)));
        }

        [HttpGet("managed")]
        public async Task<ActionResult<ApiResponse<List<JobApplicationEmployeeDto>>>> GetAllManaged()
        {
            var employee = await _currentUser.GetCurrentAsync<Employee>();
            if (employee is null)
            {
                return Unauthorized(new ApiResponse("Employee not found"));
            }

            var applications = _uow.JobApplications.GetManagedByEmployeeId(employee.Id).Select(JobApplicationMapper.ToEmployeeDto).ToList();
            return Ok(new ApiResponse<List<JobApplicationEmployeeDto>>("Managed job applications retrieved", applications));
        }

        [HttpPut("managed")]
        public async Task<ActionResult<ApiResponse>> AddToManaged([FromBody] ManageJobApplicationRequest request)
        {
            var validationResult = await _manageValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(new ApiResponse(string.Join(" ", validationResult.Errors.Select(e => e.ErrorMessage))));
            }

            var employee = await _currentUser.GetCurrentAsync<Employee>();
            if (employee is null)
            {
                return Unauthorized(new ApiResponse("Employee not found"));
            }

            var application = _uow.JobApplications.GetByIdWithDetails(request.ApplicationId);
            if (application is null)
            {
                return NotFound(new ApiResponse("Job application not found"));
            }
            if (application.IsManaged)
            {
                return Conflict(new ApiResponse("This job application is already managed"));
            }

            application.EmployeeId = employee.Id;
            application.Status = JobApplicationStatus.UNDER_REVIEW;
            _uow.JobApplications.Update(application);
            await _uow.SaveChangesAsync();

            return Ok(new ApiResponse("Job application added to managed list"));
        }

        [HttpGet("managed/{id}")]
        public async Task<ActionResult<ApiResponse<JobApplicationEmployeeDto>>> GetManaged([FromRoute] long id)
        {
            var employee = await _currentUser.GetCurrentAsync<Employee>();
            if (employee is null)
            {
                return Unauthorized(new ApiResponse("Employee not found"));
            }

            var application = _uow.JobApplications.GetByIdWithDetails(id);
            if (application is null)
            {
                return NotFound(new ApiResponse("Job application not found"));
            }
            if (!application.IsManaged)
            {
                return Conflict(new ApiResponse("This job application is not managed"));
            }
            if (application.EmployeeId != employee.Id)
            {
                return Unauthorized(new ApiResponse("Another employee is managing this job application"));
            }

            return Ok(new ApiResponse<JobApplicationEmployeeDto>("Job application retrieved", JobApplicationMapper.ToEmployeeDto(application)));
        }

        [HttpPut("managed/{id}")]
        public async Task<ActionResult<ApiResponse>> UpdateStatus([FromRoute] long id, [FromBody] UpdateJobApplicationStatusRequest request)
        {
            var validationResult = await _updateValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(new ApiResponse(string.Join(" ", validationResult.Errors.Select(e => e.ErrorMessage))));
            }

            if (request.Status == JobApplicationStatus.INTERVIEW_SCHEDULED)
            {
                return Conflict(new ApiResponse("Manually setting status to InterviewScheduled is not allowed"));
            }

            var employee = await _currentUser.GetCurrentAsync<Employee>();
            if (employee is null)
            {
                return Unauthorized(new ApiResponse("Employee not found"));
            }

            var application = _uow.JobApplications.GetByIdWithDetails(id);
            if (application is null)
            {
                return NotFound(new ApiResponse("Job application not found"));
            }
            if (!application.IsManaged)
            {
                return Conflict(new ApiResponse("This job application is not managed"));
            }
            if (application.EmployeeId != employee.Id)
            {
                return Unauthorized(new ApiResponse("Another employee is managing this job application"));
            }
            if (application.Status == JobApplicationStatus.ACCEPTED)
            {
                return Conflict(new ApiResponse("You cannot edit this application's status"));
            }
            if (!JobApplicationStatusUtil.IsStatusChangeAllowed(application.Status, request.Status))
            {
                return Conflict(new ApiResponse("Status change not allowed"));
            }

            application.Status = request.Status;
            _uow.JobApplications.Update(application);
            await _uow.SaveChangesAsync();

            return Ok(new ApiResponse("Job application status updated"));
        }

        [HttpGet("managed/{id}/candidate/profile")]
        public async Task<ActionResult<ApiResponse<CandidateDto>>> GetCandidateProfileForJobApplication([FromRoute] long id)
        {
            var employee = await _currentUser.GetCurrentAsync<Employee>();
            if (employee is null)
            {
                return Unauthorized(new ApiResponse("Employee not found"));
            }

            var application = _uow.JobApplications.GetByIdWithDetails(id);
            if (application is null)
            {
                return NotFound(new ApiResponse("Job application not found"));
            }
            if (!application.IsManaged)
            {
                return Conflict(new ApiResponse("This job application is not managed"));
            }
            if (application.EmployeeId != employee.Id)
            {
                return Unauthorized(new ApiResponse("Another employee is managing this job application"));
            }

            var candidate = _uow.Candidates.Find(c => c.Id == application.CandidateId).FirstOrDefault();
            if (candidate is null)
            {
                return NotFound(new ApiResponse("Candidate not found"));
            }

            var appUser = await _userManager.FindByIdAsync(candidate.AppUserId);
            if (appUser is null)
            {
                return NotFound(new ApiResponse("Candidate not found"));
            }

            var dto = new CandidateDto
            {
                Id = candidate.Id,
                FirstName = candidate.FirstName,
                LastName = candidate.LastName,
                Sex = candidate.Sex,
                Address = candidate.Address,
                Email = appUser.Email ?? string.Empty,
                Phone = appUser.PhoneNumber ?? string.Empty,
            };

            return Ok(new ApiResponse<CandidateDto>("Candidate profile retrieved", dto));
        }

        [HttpGet("managed/{id}/candidate/resume")]
        public async Task<IActionResult> GetCandidateResumeForJobApplication([FromRoute] long id)
        {
            var employee = await _currentUser.GetCurrentAsync<Employee>();
            if (employee is null)
            {
                return Unauthorized(new ApiResponse("Employee not found"));
            }

            var application = _uow.JobApplications.GetByIdWithDetails(id);
            if (application is null)
            {
                return NotFound(new ApiResponse("Job application not found"));
            }
            if (!application.IsManaged)
            {
                return Conflict(new ApiResponse("This job application is not managed"));
            }
            if (application.EmployeeId != employee.Id)
            {
                return Unauthorized(new ApiResponse("Another employee is managing this job application"));
            }

            var candidate = _uow.Candidates.Find(c => c.Id == application.CandidateId).FirstOrDefault();
            if (candidate is null || candidate.Resume is null || candidate.Resume.Length == 0)
            {
                return NotFound(new ApiResponse("Resume not uploaded"));
            }

            Response.Headers.ContentDisposition = "inline; filename=\"resume.pdf\"";
            return File(candidate.Resume, "application/pdf");
        }
    }
}
