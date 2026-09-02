using JobApplicationAPI.DTOs.Interviews;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobApplicationAPI.Controllers.Employees
{
    [Route("api/employees/interviews")]
    [ApiController]
    public class InterviewsController : ControllerBase
    {
        public InterviewsController()
        {

        }

        [HttpGet]
        public IActionResult GetAll([FromQuery] long jobApplicationId)
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult Schedule([FromBody] CreateInterviewRequest request)
        {
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Cancel([FromRoute] long interviewId)
        {
            return Ok();
        }
    }
}
