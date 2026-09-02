using JobApplicationAPI.DTOs;
using JobApplicationAPI.DTOs.JobPostings;
using Microsoft.AspNetCore.Mvc;

namespace JobApplicationAPI.Controllers.Candidates
{
    [Route("api/candidates/job-postings")]
    [ApiController]
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
