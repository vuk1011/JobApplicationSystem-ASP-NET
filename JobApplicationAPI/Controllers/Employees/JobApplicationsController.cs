using JobApplicationAPI.DTOs.JobApplications;
using Microsoft.AspNetCore.Mvc;

namespace JobApplicationAPI.Controllers.Employees
{
    [Route("api/employees/job-applications")]
    [ApiController]
    public class JobApplicationsController : ControllerBase
    {
        public JobApplicationsController()
        {

        }

        [HttpGet]
        public IActionResult GetAllByJobPosting([FromQuery] long jobPostingId)
        {
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] long id)
        {
            return Ok();
        }

        [HttpGet("managed")]
        public IActionResult GetAllManaged()
        {
            return Ok();
        }

        [HttpPut("managed")]
        public IActionResult AddToManaged([FromBody] ManageJobApplicationRequest request)
        {
            return Ok();
        }

        [HttpGet("managed/{id}")]
        public IActionResult GetManaged([FromRoute] long id)
        {
            return Ok();
        }

        [HttpPut("managed/{id}")]
        public IActionResult UpdateStatus([FromRoute] long id, [FromBody] UpdateJobApplicationStatusRequest request)
        {
            return Ok();
        }

        [HttpGet("managed/{id}/candidate/profile")]
        public IActionResult GetCandidateProfileForJobApplication([FromRoute] long id)
        {
            return Ok();
        }

        [HttpGet("managed/{id}/candidate/resume")]
        public IActionResult GetCandidateResumeForJobApplication([FromRoute] long id)
        {
            return Ok();
        }
    }
}
