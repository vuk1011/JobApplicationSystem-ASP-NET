using Microsoft.AspNetCore.Http;
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
        public IActionResult GetAll()
        {
            return Ok();
        }
    }
}
