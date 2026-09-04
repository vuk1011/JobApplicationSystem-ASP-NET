using Domain.Entities;
using Domain.Repositories;
using FluentValidation;
using JobApplicationAPI.DTOs;
using JobApplicationAPI.DTOs.JobPostings;
using JobApplicationAPI.Services;
using JobApplicationAPI.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace JobApplicationAPI.Controllers.Employees
{
    [ApiController]
    [Route("api/employees/job-postings")]
    [Authorize(Roles = "Employee")]
    public class JobPostingsController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        private readonly IValidator<CreateJobPostingRequest> _createValidator;
        private readonly IValidator<UpdateJobPostingRequest> _updateValidator;

        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true,
            WriteIndented = true,
            Converters = { new JsonStringEnumConverter() },
        };

        private readonly CurrentUserService _currentUser;

        public JobPostingsController(IUnitOfWork uow, IValidator<CreateJobPostingRequest> createValidator, IValidator<UpdateJobPostingRequest> updateValidator, CurrentUserService currentUser)
        {
            _uow = uow;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _currentUser = currentUser;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<JobPostingDto>>>> GetAll()
        {
            var employee = await _currentUser.GetCurrentAsync<Employee>();
            if (employee is null)
            {
                return Unauthorized(new ApiResponse("Employee not found"));
            }

            var jobPostings = _uow.JobPostings.GetAllByCompanyId(employee.CompanyId).Select(JobPostingMapper.ToDto).ToList();
            return Ok(new ApiResponse<List<JobPostingDto>>("Job postings retrieved", jobPostings));
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<JobPostingDto>>> Create([FromBody] CreateJobPostingRequest request)
        {
            Console.WriteLine(request.DateOfExpiration);

            var validationResult = await _createValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(new ApiResponse(string.Join(" ", validationResult.Errors.Select(e => e.ErrorMessage))));
            }

            var employee = await _currentUser.GetCurrentAsync<Employee>();
            if (employee is null)
            {
                return Unauthorized(new ApiResponse("Employee not found"));
            }

            if (request.DateOfExpiration < DateOnly.FromDateTime(DateTime.Today))
            {
                return Conflict(new ApiResponse("Invalid date of expiration"));
            }

            var jobPosting = CreateJobPosting(request, employee.CompanyId);
            _uow.JobPostings.Add(jobPosting);
            await _uow.SaveChangesAsync();

            var created = _uow.JobPostings.GetByIdWithCompany(jobPosting.Id)!;
            return Ok(new ApiResponse<JobPostingDto>("Job posting created", JobPostingMapper.ToDto(created)));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<JobPostingDto>>> Get([FromRoute] long id)
        {
            var employee = await _currentUser.GetCurrentAsync<Employee>();
            if (employee is null)
            {
                return Unauthorized(new ApiResponse("Employee not found"));
            }

            var jobPosting = _uow.JobPostings.GetByIdWithCompany(id);
            if (jobPosting is null)
            {
                return NotFound(new ApiResponse("Job posting not found"));
            }
            if (jobPosting.CompanyId != employee.CompanyId)
            {
                return Unauthorized(new ApiResponse("This job posting does not belong to your company"));
            }

            return Ok(new ApiResponse<JobPostingDto>("Job posting retrieved", JobPostingMapper.ToDto(jobPosting)));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse>> Update([FromRoute] long id, [FromBody] UpdateJobPostingRequest request)
        {
            var validationResult = await _updateValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(new ApiResponse(string.Join(" ", validationResult.Errors.Select(e => e.ErrorMessage))));
            }

            var employee = await _currentUser.GetCurrentAsync<Employee>();
            if (employee is null)
            {
                return Unauthorized(new ApiResponse("Employee not found"));
            }

            var jobPosting = _uow.JobPostings.GetByIdWithCompany(id);
            if (jobPosting is null)
            {
                return NotFound(new ApiResponse("Job posting not found"));
            }
            if (jobPosting.CompanyId != employee.CompanyId)
            {
                return Unauthorized(new ApiResponse("This job posting does not belong to your company"));
            }
            if (request.DateOfExpiration < DateOnly.FromDateTime(DateTime.Today))
            {
                return Conflict(new ApiResponse("Expiration date cannot be set before current time"));
            }

            jobPosting.Title = request.Title;
            jobPosting.Description = request.Description;
            jobPosting.DateOfExpiration = request.DateOfExpiration;
            _uow.JobPostings.Update(jobPosting);
            await _uow.SaveChangesAsync();

            return Ok(new ApiResponse("Job posting updated"));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse>> Delete([FromRoute] long id)
        {
            var employee = await _currentUser.GetCurrentAsync<Employee>();
            if (employee is null)
            {
                return Unauthorized(new ApiResponse("Employee not found"));
            }

            var jobPosting = _uow.JobPostings.GetByIdWithCompany(id);
            if (jobPosting is null)
            {
                return NotFound(new ApiResponse("Job posting not found"));
            }
            if (jobPosting.CompanyId != employee.CompanyId)
            {
                return Unauthorized(new ApiResponse("This job posting does not belong to your company"));
            }

            _uow.JobPostings.Remove(jobPosting);
            await _uow.SaveChangesAsync();

            return Ok(new ApiResponse("Job posting deleted"));
        }

        [HttpGet("export")]
        public async Task<IActionResult> Export()
        {
            var employee = await _currentUser.GetCurrentAsync<Employee>();
            if (employee is null)
            {
                return Unauthorized(new ApiResponse("Employee not found"));
            }

            var jobPostings = _uow.JobPostings.GetAllByCompanyId(employee.CompanyId).Select(JobPostingMapper.ToDto).ToList();
            var json = JsonSerializer.Serialize(jobPostings, JsonOptions);

            return File(System.Text.Encoding.UTF8.GetBytes(json), "application/json", "job-postings.json");
        }

        [HttpPost("import")]
        public async Task<ActionResult<ApiResponse<List<JobPostingDto>>>> Import(IFormFile file)
        {
            if (file is null || file.Length == 0)
            {
                return BadRequest(new ApiResponse("File is empty"));
            }

            var employee = await _currentUser.GetCurrentAsync<Employee>();
            if (employee is null)
            {
                return Unauthorized(new ApiResponse("Employee not found"));
            }

            List<CreateJobPostingRequest>? requests;
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                var json = await reader.ReadToEndAsync();
                try
                {
                    requests = JsonSerializer.Deserialize<List<CreateJobPostingRequest>>(json, JsonOptions);
                }
                catch (JsonException)
                {
                    return BadRequest(new ApiResponse("Invalid JSON file"));
                }
            }

            var imported = new List<JobPostingDto>();
            foreach (var request in requests ?? [])
            {
                if (request.DateOfExpiration < DateOnly.FromDateTime(DateTime.Today))
                {
                    return Conflict(new ApiResponse("Invalid date of expiration"));
                }

                var jobPosting = CreateJobPosting(request, employee.CompanyId);
                _uow.JobPostings.Add(jobPosting);
                await _uow.SaveChangesAsync();

                var created = _uow.JobPostings.GetByIdWithCompany(jobPosting.Id)!;
                imported.Add(JobPostingMapper.ToDto(created));
            }

            return Ok(new ApiResponse<List<JobPostingDto>>($"{imported.Count} job posting(s) imported", imported));
        }

        private static JobPosting CreateJobPosting(CreateJobPostingRequest request, long companyId) => new()
        {
            Title = request.Title,
            Description = request.Description,
            DateOfPublishing = DateOnly.FromDateTime(DateTime.Today),
            DateOfExpiration = request.DateOfExpiration,
            CompanyId = companyId,
        };
    }
}
