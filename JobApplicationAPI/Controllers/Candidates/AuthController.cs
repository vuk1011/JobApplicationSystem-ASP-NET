using JobApplicationAPI.DTOs.Users;
using Microsoft.AspNetCore.Mvc;

namespace JobApplicationAPI.Controllers.Candidates
{
    [Route("api/candidates/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public AuthController()
        {

        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            return Ok();
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterCandidateRequest request)
        {
            return Ok();
        }
    }
}
