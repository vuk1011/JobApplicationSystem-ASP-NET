using Domain.Entities;
using Domain.Repositories;
using FluentValidation;
using JobApplicationAPI.DTOs;
using JobApplicationAPI.DTOs.Interviews;
using JobApplicationAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobApplicationAPI.Controllers.Employees
{
    [ApiController]
    [Route("api/employees/interviews")]
    [Authorize(Roles = "Employee")]
    public class InterviewsController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IValidator<CreateInterviewRequest> _createValidator;
        private readonly CurrentUserService _currentUser;

        public InterviewsController(IUnitOfWork uow, IValidator<CreateInterviewRequest> createValidator, CurrentUserService currentUser)
        {
            _uow = uow;
            _createValidator = createValidator;
            _currentUser = currentUser;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<InterviewDto>>>> GetAll([FromQuery] long jobApplicationId)
        {
            var employee = await _currentUser.GetCurrentAsync<Employee>();
            if (employee is null)
            {
                return Unauthorized(new ApiResponse("Employee not found"));
            }

            var application = _uow.JobApplications.GetByIdWithDetails(jobApplicationId);
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

            var interviews = _uow.Interviews.GetByJobApplicationId(jobApplicationId)
                .Select(i => new InterviewDto { Id = i.Id, Title = i.Title, Description = i.Description, TimeScheduled = i.TimeScheduled })
                .ToList();

            return Ok(new ApiResponse<List<InterviewDto>>("Interviews retrieved", interviews));
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse>> Schedule([FromBody] CreateInterviewRequest request)
        {
            var validationResult = await _createValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(new ApiResponse(string.Join(" ", validationResult.Errors.Select(e => e.ErrorMessage))));
            }

            var employee = await _currentUser.GetCurrentAsync<Employee>();
            if (employee is null)
            {
                return Unauthorized(new ApiResponse("Employee not found"));
            }

            var application = _uow.JobApplications.GetByIdWithDetails(request.JobApplicationId);
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
            if (!JobApplicationStatusUtil.IsStatusChangeAllowed(application.Status, JobApplicationStatus.INTERVIEW_SCHEDULED))
            {
                return Conflict(new ApiResponse("Interview cannot be scheduled from current status"));
            }

            application.Status = JobApplicationStatus.INTERVIEW_SCHEDULED;
            _uow.JobApplications.Update(application);

            var interview = new Interview
            {
                Title = request.Title,
                Description = request.Description,
                TimeScheduled = request.TimeScheduled,
                JobApplicationId = request.JobApplicationId,
            };
            _uow.Interviews.Add(interview);
            await _uow.SaveChangesAsync();

            return Ok(new ApiResponse("Interview scheduled"));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse>> Cancel([FromRoute] long id)
        {
            var employee = await _currentUser.GetCurrentAsync<Employee>();
            if (employee is null)
            {
                return Unauthorized(new ApiResponse("Employee not found"));
            }

            var interview = _uow.Interviews.GetByIdWithJobApplication(id);
            if (interview is null)
            {
                return NotFound(new ApiResponse("Interview not found"));
            }
            if (interview.JobApplication.EmployeeId != employee.Id)
            {
                return Unauthorized(new ApiResponse("Another employee is managing the associated job application for the interview"));
            }
            if (interview.TimeScheduled < DateTime.Now)
            {
                return Conflict(new ApiResponse("Interview cannot be deleted after it took place"));
            }

            _uow.Interviews.Remove(interview);
            await _uow.SaveChangesAsync();

            return Ok(new ApiResponse("Interview cancelled"));
        }
    }
}
