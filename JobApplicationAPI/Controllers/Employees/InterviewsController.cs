using JobApplicationAPI.DTOs;
using JobApplicationAPI.DTOs.Interviews;
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
        public ActionResult<ApiResponse<List<InterviewDto>>> GetAll([FromQuery] long jobApplicationId)
        {
            return Ok();
        }

        [HttpPost]
        public ActionResult<ApiResponse> Schedule([FromBody] CreateInterviewRequest request)
        {
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult<ApiResponse> Cancel([FromRoute] long interviewId)
        {
            return Ok();
        }
    }
}
