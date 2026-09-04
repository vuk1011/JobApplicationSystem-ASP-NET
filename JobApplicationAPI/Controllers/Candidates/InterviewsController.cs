using Domain.Entities;
using Domain.Repositories;
using JobApplicationAPI.DTOs;
using JobApplicationAPI.DTOs.Interviews;
using JobApplicationAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobApplicationAPI.Controllers.Candidates
{
    [ApiController]
    [Route("api/candidates/interviews")]
    [Authorize(Roles = "Candidate")]
    public class InterviewsController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly CurrentUserService _currentUser;

        public InterviewsController(IUnitOfWork uow, CurrentUserService currentUser)
        {
            _uow = uow;
            _currentUser = currentUser;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<InterviewDto>>>> GetAll([FromQuery] long jobApplicationId)
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

            var interviews = _uow.Interviews.GetByJobApplicationId(jobApplicationId)
                .Select(i => new InterviewDto { Id = i.Id, Title = i.Title, Description = i.Description, TimeScheduled = i.TimeScheduled })
                .ToList();

            return Ok(new ApiResponse<List<InterviewDto>>("Successfully retrieved interviews for job application", interviews));
        }
    }
}
