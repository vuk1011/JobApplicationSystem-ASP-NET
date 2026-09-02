using JobApplicationAPI.DTOs;
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
        public ActionResult<ApiResponse> Submit([FromBody] SubmitJobApplicationRequest request)
        {
            return Ok();
        }

        [HttpGet]
        public ActionResult<ApiResponse<List<JobApplicationCandidateDto>>> GetAll()
        {
            return Ok();
        }

        [HttpGet("{id}")]
        public ActionResult<ApiResponse<JobApplicationCandidateDto>> Get([FromRoute] long id)
        {
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult<ApiResponse> Withdraw([FromRoute] long id)
        {
            return Ok();
        }
    }
}
