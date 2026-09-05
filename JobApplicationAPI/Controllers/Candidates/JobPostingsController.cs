using Domain.Repositories;
using JobApplicationAPI.DTOs;
using JobApplicationAPI.DTOs.JobPostings;
using JobApplicationAPI.Utilities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobApplicationAPI.Controllers.Candidates
{
    [ApiController]
    [Route("api/candidates/job-postings")]
    [Authorize(Roles = "Candidate")]
    public class JobPostingsController : ControllerBase
    {
        private readonly IMediator _mediator;

        private readonly IUnitOfWork _uow;

        public JobPostingsController(IMediator mediator, IUnitOfWork uow)
        {
            _mediator = mediator;
            _uow = uow;
        }

        [HttpGet]
        public ActionResult<ApiResponse<List<JobPostingDto>>> GetAll()
        {
            var jobPostings = _uow.JobPostings.GetAllPublished().Select(JobPostingMapper.ToDto).ToList();
            return Ok(new ApiResponse<List<JobPostingDto>>("Job postings retrieved", jobPostings));
        }
    }
}
