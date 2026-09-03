using FluentValidation;
using JobApplicationAPI.DTOs;
using JobApplicationAPI.DTOs.JobApplications;
using JobApplicationAPI.DTOs.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobApplicationAPI.Controllers.Employees
{
    [ApiController]
    [Route("api/employees/job-applications")]
    [Authorize(Roles = "Employee")]
    public class JobApplicationsController : ControllerBase
    {
        private readonly IValidator<ManageJobApplicationRequest> _manageValidator;
        private readonly IValidator<UpdateJobApplicationStatusRequest> _updateValidator;

        public JobApplicationsController(IValidator<ManageJobApplicationRequest> manageValidator, IValidator<UpdateJobApplicationStatusRequest> updateValidator)
        {
            _manageValidator = manageValidator;
            _updateValidator = updateValidator;
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
