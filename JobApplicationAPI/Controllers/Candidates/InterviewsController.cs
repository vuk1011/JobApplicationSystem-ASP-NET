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
        public IActionResult GetAll([FromQuery] long jobApplicationId)
        {
            return Ok();
        }
    }
}
