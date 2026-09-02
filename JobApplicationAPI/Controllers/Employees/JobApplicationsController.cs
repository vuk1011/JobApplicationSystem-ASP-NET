using JobApplicationAPI.DTOs;
using JobApplicationAPI.DTOs.JobApplications;
using JobApplicationAPI.DTOs.Users;
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
        public ActionResult<ApiResponse<List<JobApplicationEmployeeDto>>> GetAllByJobPosting([FromQuery] long jobPostingId)
        {
            return Ok();
        }

        [HttpGet("{id}")]
        public ActionResult<ApiResponse<JobApplicationEmployeeDto>> Get([FromRoute] long id)
        {
            return Ok();
        }

        [HttpGet("managed")]
        public ActionResult<ApiResponse<List<JobApplicationEmployeeDto>>> GetAllManaged()
        {
            return Ok();
        }

        [HttpPut("managed")]
        public ActionResult<ApiResponse> AddToManaged([FromBody] ManageJobApplicationRequest request)
        {
            return Ok();
        }

        [HttpGet("managed/{id}")]
        public ActionResult<ApiResponse<JobApplicationEmployeeDto>> GetManaged([FromRoute] long id)
        {
            return Ok();
        }

        [HttpPut("managed/{id}")]
        public ActionResult<ApiResponse> UpdateStatus([FromRoute] long id, [FromBody] UpdateJobApplicationStatusRequest request)
        {
            return Ok();
        }

        [HttpGet("managed/{id}/candidate/profile")]
        public ActionResult<ApiResponse<CandidateDto>> GetCandidateProfileForJobApplication([FromRoute] long id)
        {
            return Ok();
        }

        [HttpGet("managed/{id}/candidate/resume")]
        public async Task<FileResult> GetCandidateResumeForJobApplication([FromRoute] long id)
        {
            return File(Array.Empty<byte>(), "application/pdf", "resume.pdf");
        }
    }
}
