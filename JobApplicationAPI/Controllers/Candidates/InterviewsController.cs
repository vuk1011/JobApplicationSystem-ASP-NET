using JobApplicationAPI.DTOs;
using JobApplicationAPI.DTOs.Interviews;
using Microsoft.AspNetCore.Mvc;

namespace JobApplicationAPI.Controllers.Candidates
{
    [Route("api/candidates/interviews")]
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
    }
}
