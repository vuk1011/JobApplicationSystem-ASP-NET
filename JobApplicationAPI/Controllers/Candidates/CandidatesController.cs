using Domain.Entities;
using Domain.Repositories;
using FluentValidation;
using Infrastructure.Identity;
using JobApplicationAPI.DTOs;
using JobApplicationAPI.DTOs.Users;
using JobApplicationAPI.Services;
using JobApplicationAPI.Utilities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JobApplicationAPI.Controllers.Candidates
{
    [ApiController]
    [Route("api/candidates/me")]
    [Authorize(Roles = "Candidate")]
    public class CandidatesController : ControllerBase
    {
        private readonly IMediator _mediator;

        private readonly IUnitOfWork _uow;
        private readonly UserManager<AppUser> _userManager;
        private readonly IValidator<UpdateCandidateRequest> _updateCandidateValidator;
        private readonly CurrentUserService _currentUser;

        public CandidatesController(IMediator mediator, IUnitOfWork uow, UserManager<AppUser> userManager, IValidator<UpdateCandidateRequest> updateCandidateValidator, CurrentUserService currentUser)
        {
            _mediator = mediator;

            _uow = uow;
            _userManager = userManager;
            _updateCandidateValidator = updateCandidateValidator;
            _currentUser = currentUser;
        }

        [HttpGet("resume")]
        public async Task<IActionResult> GetResume()
        {
            var candidate = await _currentUser.GetCurrentAsync<Candidate>();
            if (candidate is null || candidate.Resume is null)
            {
                return NotFound(new ApiResponse("Resume not uploaded"));
            }

            Response.Headers.ContentDisposition = "inline; filename=\"resume.pdf\"";
            return File(candidate.Resume, "application/pdf");
        }

        [HttpPut("resume")]
        public async Task<ActionResult<ApiResponse>> UploadResume(IFormFile file)
        {
            if (file is null || file.ContentType != "application/pdf")
            {
                return BadRequest(new ApiResponse("Only PDF resumes are allowed"));
            }

            var candidate = await _currentUser.GetCurrentAsync<Candidate>();
            if (candidate is null)
            {
                return Unauthorized(new ApiResponse("Candidate not found"));
            }

            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            candidate.Resume = memoryStream.ToArray();
            _uow.Candidates.Update(candidate);
            await _uow.SaveChangesAsync();

            return Ok(new ApiResponse("Resume uploaded successfully"));
        }

        [HttpDelete("resume")]
        public async Task<ActionResult<ApiResponse>> DeleteResume()
        {
            var candidate = await _currentUser.GetCurrentAsync<Candidate>();
            if (candidate is null)
            {
                return Unauthorized(new ApiResponse("Candidate not found"));
            }

            candidate.Resume = null;
            _uow.Candidates.Update(candidate);
            await _uow.SaveChangesAsync();

            return Ok(new ApiResponse("Resume deleted successfully"));
        }

        [HttpGet("profile")]
        public async Task<ActionResult<ApiResponse<CandidateDto>>> GetProfile()
        {
            var candidate = await _currentUser.GetCurrentAsync<Candidate>();
            if (candidate is null)
            {
                return Unauthorized(new ApiResponse("Candidate not found"));
            }

            var appUser = await _userManager.FindByIdAsync(candidate.AppUserId);
            if (appUser is null)
            {
                return NotFound(new ApiResponse("Candidate not found"));
            }

            return Ok(new ApiResponse<CandidateDto>("Profile information retrieved successfully", CandidateMapper.ToDto(candidate, appUser)));
        }

        [HttpPut("profile")]
        public async Task<ActionResult<ApiResponse<CandidateDto>>> UpdateProfile([FromBody] UpdateCandidateRequest request)
        {
            var validationResult = await _updateCandidateValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(new ApiResponse(string.Join(" ", validationResult.Errors.Select(e => e.ErrorMessage))));
            }

            var candidate = await _currentUser.GetCurrentAsync<Candidate>();
            if (candidate is null)
            {
                return Unauthorized(new ApiResponse("Candidate not found"));
            }

            var appUser = await _userManager.FindByIdAsync(candidate.AppUserId);
            if (appUser is null)
            {
                return NotFound(new ApiResponse("Candidate not found"));
            }

            candidate.FirstName = request.FirstName;
            candidate.LastName = request.LastName;
            candidate.Sex = request.Sex;
            candidate.Address = request.Address;
            _uow.Candidates.Update(candidate);
            await _uow.SaveChangesAsync();

            appUser.PhoneNumber = request.Phone;
            await _userManager.UpdateAsync(appUser);

            return Ok(new ApiResponse<CandidateDto>("Profile information updated successfully", CandidateMapper.ToDto(candidate, appUser)));
        }
    }
}
