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

namespace JobApplicationAPI.Controllers.Employees
{
    [ApiController]
    [Route("api/employees/auth")]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        private readonly UserManager<AppUser> _userManager;
        private readonly JwtService _jwtService;

        private readonly IValidator<LoginRequest> _loginValidator;
        private readonly IValidator<RegisterEmployeeRequest> _registerValidator;

        public AuthController(
            UserManager<AppUser> userManager,
            JwtService jwtService,
            IUnitOfWork uow,
            IValidator<LoginRequest> loginValidator,
            IValidator<RegisterEmployeeRequest> registerValidator)
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
            if (user is null || user.UserType != UserType.Employee || !await _userManager.CheckPasswordAsync(user, request.Password))
            {
                return Unauthorized(new ApiResponse("Invalid email or password"));
            }

            var employee = await _uow.Employees.GetByAppUserIdAsync(user.Id);
            if (employee is null)
            {
                return Unauthorized(new ApiResponse("Invalid email or password"));
            }

            var token = await _jwtService.CreateTokenAsync(user, $"{employee.FirstName} {employee.LastName}");
            return Ok(new ApiResponse<LoginSuccessResponse>("Login successful", new LoginSuccessResponse
            {
                Jwt = token,
                FirstName = employee.FirstName,
            }));
        }

        [HttpPost("register")]
        public async Task<ActionResult<ApiResponse>> Register([FromBody] RegisterEmployeeRequest request)
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
                UserType = UserType.Employee,
            };

            var createResult = await _userManager.CreateAsync(user, request.Password);
            if (!createResult.Succeeded)
            {
                return BadRequest(new ApiResponse(string.Join(" ", createResult.Errors.Select(e => e.Description))));
            }

            await _userManager.AddToRoleAsync(user, "Employee");

            var employee = new Employee
            {
                AppUserId = user.Id,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Sex = request.Sex,
                Address = request.Address,
                NationalId = request.NationalId,
                DateOfBirth = request.DateOfBirth,
                DateOfHire = request.DateOfHire,
                CompanyId = request.CompanyId,
            };
            _uow.Employees.Add(employee);
            await _uow.SaveChangesAsync();

            return Ok(new ApiResponse("Registration successful"));
        }
    }
}
