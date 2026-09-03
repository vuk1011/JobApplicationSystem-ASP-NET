using FluentValidation;
using JobApplicationAPI.DTOs;
using JobApplicationAPI.DTOs.JobPostings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobApplicationAPI.Controllers.Employees
{
    [ApiController]
    [Route("api/employees/job-postings")]
    [Authorize(Roles = "Employee")]
    public class JobPostingsController : ControllerBase
    {
        private readonly IValidator<CreateJobPostingRequest> _createValidator;
        private readonly IValidator<UpdateJobPostingRequest> _updateValidator;

        public JobPostingsController(IValidator<CreateJobPostingRequest> createValidator, IValidator<UpdateJobPostingRequest> updateValidator)
        {
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        [HttpGet]
        public ActionResult<ApiResponse<List<JobPostingDto>>> GetAll()
        {
            return Ok();
        }

        [HttpPost]
        public ActionResult<ApiResponse<JobPostingDto>> Create([FromBody] CreateJobPostingRequest request)
        {
            return Ok();
        }

        [HttpGet("{id}")]
        public ActionResult<ApiResponse<JobPostingDto>> Get([FromRoute] long id)
        {
            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult<ApiResponse> Update([FromRoute] long id, [FromBody] UpdateJobPostingRequest request)
        {
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult<ApiResponse> Delete([FromRoute] long id)
        {
            return Ok();
        }

        [HttpGet("export")]
        public async Task<IActionResult> Export()
        {
            return Ok();
        }

        [HttpPost("import")]
        public async Task<ActionResult<ApiResponse>> Import(IFormFile file)
        {
            return Ok();
        }
    }
}
