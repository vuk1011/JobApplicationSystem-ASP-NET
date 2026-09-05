using Domain.Entities;
using Domain.Repositories;
using FluentValidation;
using JobApplicationAPI.DTOs;
using JobApplicationAPI.DTOs.Offers;
using JobApplicationAPI.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobApplicationAPI.Controllers.Employees
{
    [ApiController]
    [Route("api/employees/offers")]
    [Authorize(Roles = "Employee")]
    public class OffersController : ControllerBase
    {
        private readonly IMediator _mediator;

        private readonly IUnitOfWork _uow;
        private readonly IValidator<CreateOfferRequest> _createValidator;
        private readonly CurrentUserService _currentUser;

        public OffersController(IMediator mediator, IUnitOfWork uow, IValidator<CreateOfferRequest> createValidator, CurrentUserService currentUser)
        {
            _mediator = mediator;
            _uow = uow;
            _createValidator = createValidator;
            _currentUser = currentUser;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<OfferDto>>>> GetAll([FromQuery] long jobApplicationId)
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

            var offers = _uow.Offers.GetByJobApplicationId(jobApplicationId)
                .Select(o => new OfferDto { Id = o.Id, Name = o.Name, Accepted = o.Accepted })
                .ToList();

            return Ok(new ApiResponse<List<OfferDto>>("Offers retrieved", offers));
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse>> Create([FromBody] CreateOfferRequest request)
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
            if (!JobApplicationStatusUtil.IsStatusChangeAllowed(application.Status, JobApplicationStatus.OFFERED))
            {
                return Conflict(new ApiResponse("Offer cannot be created in current status"));
            }

            application.Status = JobApplicationStatus.OFFERED;
            _uow.JobApplications.Update(application);

            var offer = new Offer
            {
                Name = request.Name,
                JobApplicationId = request.JobApplicationId,
            };
            _uow.Offers.Add(offer);
            await _uow.SaveChangesAsync();

            return Ok(new ApiResponse("Offer created"));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse>> Delete([FromRoute] long id)
        {
            var employee = await _currentUser.GetCurrentAsync<Employee>();
            if (employee is null)
            {
                return Unauthorized(new ApiResponse("Employee not found"));
            }

            var offer = _uow.Offers.GetByIdWithJobApplication(id);
            if (offer is null)
            {
                return NotFound(new ApiResponse("Offer not found"));
            }
            if (offer.JobApplication.EmployeeId != employee.Id)
            {
                return Unauthorized(new ApiResponse("Another employee is managing the associated job application for the offer"));
            }
            if (offer.Accepted is not null)
            {
                return Conflict(new ApiResponse("Offer cannot be deleted after it got accepted or rejected"));
            }

            _uow.Offers.Remove(offer);
            await _uow.SaveChangesAsync();

            return Ok(new ApiResponse("Offer deleted"));
        }
    }
}
