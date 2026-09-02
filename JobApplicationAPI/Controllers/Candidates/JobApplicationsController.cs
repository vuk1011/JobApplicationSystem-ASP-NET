using JobApplicationAPI.DTOs.JobApplications;
using Microsoft.AspNetCore.Mvc;

namespace JobApplicationAPI.Controllers.Candidates
{
    [Route("api/candidates/job-applications")]
    [ApiController]
    public class JobApplicationsController : ControllerBase
    {
        public JobApplicationsController()
        {

        }

        [HttpPost]
        public IActionResult Submit([FromBody] SubmitJobApplicationRequest request)
        {
            return Ok();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] long id)
        {
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Withdraw([FromRoute] long id)
        {
            return Ok();
        }
    }
}
