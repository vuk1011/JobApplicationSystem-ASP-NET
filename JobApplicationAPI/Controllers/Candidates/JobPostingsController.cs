using Domain.Entities;
using Domain.Repositories;
using JobApplicationAPI.DTOs;
using JobApplicationAPI.DTOs.JobPostings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobApplicationAPI.Controllers.Candidates
{
    [ApiController]
    [Route("api/candidates/job-postings")]
    [Authorize(Roles = "Candidate")]
    public class JobPostingsController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        public JobPostingsController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpGet]
        public ActionResult<ApiResponse<List<JobPostingDto>>> GetAll()
        {
            var jobPostings = _uow.JobPostings.GetAllPublished().Select(ToDto).ToList();
            return Ok(new ApiResponse<List<JobPostingDto>>("Job postings retrieved", jobPostings));
        }

        private static JobPostingDto ToDto(JobPosting jobPosting) => new()
        {
            Id = jobPosting.Id,
            Title = jobPosting.Title,
            Description = jobPosting.Description,
            DatePublished = jobPosting.DatePublished,
            DateExpires = jobPosting.DateExpires,
            IsClosed = jobPosting.IsClosed,
            CompanyName = jobPosting.Company?.Name ?? string.Empty,
        };
    }
}
