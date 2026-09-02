using JobApplicationAPI.DTOs.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobApplicationAPI.Controllers.Candidates
{
    [Route("api/candidates/me")]
    [ApiController]
    public class CandidatesController : ControllerBase
    {
        public CandidatesController()
        {

        }

        [HttpGet("resume")]
        public IActionResult GetResume()
        {
            return Ok();
        }

        [HttpPut("resume")]
        public IActionResult UploadResume(IFormFile file)
        {
            return Ok();
        }

        [HttpDelete("resume")]
        public IActionResult DeleteResume()
        {
            return Ok();
        }

        [HttpGet("profile")]
        public IActionResult GetProfile()
        {
            return Ok();
        }

        [HttpPut("profile")]
        public IActionResult UpdateProfile([FromBody] UpdateCandidateRequest request)
        {
            return Ok();
        }
    }
}
