using JobApplicationAPI.DTOs.JobPostings;
using Microsoft.AspNetCore.Mvc;

namespace JobApplicationAPI.Controllers.Employees
{
    [Route("api/employees/job-postings")]
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

        [HttpPost]
        public IActionResult Create([FromBody] CreateJobPostingRequest request)
        {
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] long id)
        {
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] long id, [FromBody] UpdateJobPostingRequest request)
        {
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] long id)
        {
            return Ok();
        }

        [HttpGet("export")]
        public IActionResult Export()
        {
            return Ok();
        }

        [HttpPost("import")]
        public IActionResult Import(IFormFile file)
        {
            return Ok();
        }
    }
}
