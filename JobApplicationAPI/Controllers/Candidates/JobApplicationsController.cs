using FluentValidation;
using JobApplicationAPI.DTOs;
using JobApplicationAPI.DTOs.JobApplications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobApplicationAPI.Controllers.Candidates
{
    [ApiController]
    [Route("api/candidates/job-applications")]
    [Authorize(Roles = "Candidate")]
    public class JobApplicationsController : ControllerBase
    {
        private readonly IValidator<SubmitJobApplicationRequest> _submitValidator;

        public JobApplicationsController(IValidator<SubmitJobApplicationRequest> submitValidator)
        {
            _submitValidator = submitValidator;
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
