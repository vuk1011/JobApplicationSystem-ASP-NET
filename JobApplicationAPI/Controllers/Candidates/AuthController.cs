using Domain.Entities;
using Domain.Repositories;
using FluentValidation;
using Infrastructure.Identity;
using JobApplicationAPI.DTOs;
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
        private readonly IUnitOfWork _uow;
        private readonly IValidator<LoginRequest> _loginValidator;
        private readonly IValidator<RegisterCandidateRequest> _registerValidator;

        public AuthController(
            UserManager<AppUser> userManager,
            JwtService jwtService,
            IUnitOfWork uow,
            IValidator<LoginRequest> loginValidator,
            IValidator<RegisterCandidateRequest> registerValidator)
        {
            _userManager = userManager;
            _jwtService = jwtService;
            _uow = uow;
            _loginValidator = loginValidator;
            _registerValidator = registerValidator;
        }

        [HttpPost("login")]
        public async Task<ActionResult<ApiResponse<LoginSuccessResponse>>> Login([FromBody] LoginRequest request)
        {
            var validationResult = await _loginValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(new ApiResponse(string.Join(" ", validationResult.Errors.Select(e => e.ErrorMessage))));
            }

            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null || user.UserType != UserType.Candidate || !await _userManager.CheckPasswordAsync(user, request.Password))
            {
                return Unauthorized(new ApiResponse("Invalid email or password"));
            }

            var candidate = await _uow.Candidates.GetByAppUserIdAsync(user.Id);
            if (candidate is null)
            {
                return Unauthorized(new ApiResponse("Invalid email or password"));
            }

            var token = await _jwtService.CreateTokenAsync(user, $"{candidate.FirstName} {candidate.LastName}");
            return Ok(new ApiResponse<LoginSuccessResponse>("Login successful", new LoginSuccessResponse
            {
                Token = token,
                FirstName = candidate.FirstName,
            }));
        }

        [HttpPost("register")]
        public async Task<ActionResult<ApiResponse<LoginSuccessResponse>>> Register([FromBody] RegisterCandidateRequest request)
        {
            var validationResult = await _registerValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(new ApiResponse(string.Join(" ", validationResult.Errors.Select(e => e.ErrorMessage))));
            }

            var user = new AppUser
            {
                UserName = request.Email,
                Email = request.Email,
                PhoneNumber = request.Phone,
                UserType = UserType.Candidate,
            };

            var createResult = await _userManager.CreateAsync(user, request.Password);
            if (!createResult.Succeeded)
            {
                return BadRequest(new ApiResponse(string.Join(" ", createResult.Errors.Select(e => e.Description))));
            }

            await _userManager.AddToRoleAsync(user, "Candidate");

            var candidate = new Candidate
            {
                AppUserId = user.Id,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Sex = request.Sex,
                Address = request.Address,
            };
            _uow.Candidates.Add(candidate);
            await _uow.SaveChangesAsync();

            var token = await _jwtService.CreateTokenAsync(user, $"{candidate.FirstName} {candidate.LastName}");
            return Ok(new ApiResponse<LoginSuccessResponse>("Registration successful", new LoginSuccessResponse
            {
                Token = token,
                FirstName = candidate.FirstName,
            }));
        }
    }
}
