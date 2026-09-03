using FluentValidation;
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
        private readonly UserManager<AppUser> _userManager;
        private readonly JwtService _jwtService;
        private readonly IValidator<LoginRequest> _loginValidator;
        private readonly IValidator<RegisterCandidateRequest> _registerValidator;

        public AuthController(UserManager<AppUser> userManager, JwtService jwtService, IValidator<LoginRequest> loginValidator, IValidator<RegisterCandidateRequest> registerValidator)
        {
            _userManager = userManager;
            _jwtService = jwtService;
            _loginValidator = loginValidator;
            _registerValidator = registerValidator;
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
