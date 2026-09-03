using Infrastructure.Identity;
using JobApplicationAPI.DTOs.Users;
using JobApplicationAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JobApplicationAPI.Controllers.Candidates
{
    [ApiController]
    [Route("api/candidates/auth")]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> userManager;
        private readonly JwtService jwtService;

        public AuthController(UserManager<AppUser> userManager, JwtService jwtService)
        {
            this.userManager = userManager;
            this.jwtService = jwtService;
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
