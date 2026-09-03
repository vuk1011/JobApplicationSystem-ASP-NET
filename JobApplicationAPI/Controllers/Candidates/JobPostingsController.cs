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
        public JobPostingsController()
        {

        }

        [HttpGet]
        public ActionResult<ApiResponse<List<JobPostingDto>>> GetAll()
        {
            return Ok();
        }
    }
}
