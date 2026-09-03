using JobApplicationAPI.DTOs;
using JobApplicationAPI.DTOs.Interviews;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobApplicationAPI.Controllers.Candidates
{
    [ApiController]
    [Route("api/candidates/interviews")]
    [Authorize(Roles = "Candidate")]
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
