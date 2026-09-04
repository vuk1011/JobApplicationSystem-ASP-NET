using Domain.Entities;
using Domain.Repositories;
using FluentValidation;
using JobApplicationAPI.DTOs;
using JobApplicationAPI.DTOs.Offers;
using JobApplicationAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobApplicationAPI.Controllers.Candidates
{
    [ApiController]
    [Route("api/candidates/offers")]
    [Authorize(Roles = "Candidate")]
    public class OffersController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IValidator<UpdateOfferRequest> _updateValidator;
        private readonly CurrentUserService _currentUser;

        public OffersController(IUnitOfWork uow, IValidator<UpdateOfferRequest> updateValidator, CurrentUserService currentUser)
        {
            _uow = uow;
            _updateValidator = updateValidator;
            _currentUser = currentUser;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<OfferDto>>>> GetAll([FromQuery] long jobApplicationId)
        {
            var candidate = await _currentUser.GetCurrentAsync<Candidate>();
            if (candidate is null)
            {
                return Unauthorized(new ApiResponse("Candidate not found"));
            }

            var application = _uow.JobApplications.GetByIdWithDetails(jobApplicationId);
            if (application is null)
            {
                return NotFound(new ApiResponse("Job application not found"));
            }
            if (application.CandidateId != candidate.Id)
            {
                return Unauthorized(new ApiResponse("You're unauthorized for this job application"));
            }

            var offers = _uow.Offers.GetByJobApplicationId(jobApplicationId)
                .Select(o => new OfferDto { Id = o.Id, Name = o.Name, Accepted = o.Accepted })
                .ToList();

            return Ok(new ApiResponse<List<OfferDto>>("Successfully retrieved offers for job application", offers));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse>> Update([FromRoute] long id, [FromBody] UpdateOfferRequest request)
        {
            var validationResult = await _updateValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(new ApiResponse(string.Join(" ", validationResult.Errors.Select(e => e.ErrorMessage))));
            }

            var candidate = await _currentUser.GetCurrentAsync<Candidate>();
            if (candidate is null)
            {
                return Unauthorized(new ApiResponse("Candidate not found"));
            }

            var offer = _uow.Offers.GetByIdWithJobApplication(id);
            if (offer is null)
            {
                return NotFound(new ApiResponse("Offer not found"));
            }

            var application = offer.JobApplication;
            if (application.CandidateId != candidate.Id)
            {
                return Unauthorized(new ApiResponse("You're unauthorized for this job application"));
            }
            if (application.Status == JobApplicationStatus.ACCEPTED)
            {
                return Conflict(new ApiResponse("Offer cannot be updated in application's final status"));
            }
            if (offer.Accepted is not null)
            {
                return Conflict(new ApiResponse("Offer cannot be updated after it got accepted or rejected"));
            }

            var targetStatus = request.Accepted ? JobApplicationStatus.ACCEPTED : JobApplicationStatus.REJECTED;
            if (!JobApplicationStatusUtil.IsStatusChangeAllowed(application.Status, targetStatus))
            {
                return Conflict(new ApiResponse("Offer cannot be updated when job application is in current status"));
            }

            offer.Accepted = request.Accepted;
            application.Status = targetStatus;
            _uow.Offers.Update(offer);
            _uow.JobApplications.Update(application);
            await _uow.SaveChangesAsync();

            return Ok(new ApiResponse("Successfully updated offer"));
        }
    }
}
