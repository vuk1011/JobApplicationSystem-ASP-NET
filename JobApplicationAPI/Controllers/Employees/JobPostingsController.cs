using JobApplicationAPI.Commands.JobPostings;
using JobApplicationAPI.DTOs;
using JobApplicationAPI.DTOs.JobPostings;
using JobApplicationAPI.Queries.JobPostings;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace JobApplicationAPI.Controllers.Employees
{
    [ApiController]
    [Route("api/employees/job-postings")]
    [Authorize(Roles = "Employee")]
    public class JobPostingsController : ControllerBase
    {
        private readonly IMediator _mediator;

        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true,
            WriteIndented = true,
            Converters = { new JsonStringEnumConverter() },
        };

        public JobPostingsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<JobPostingDto>>>> GetAll()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var jobPostings = await _mediator.Send(new GetJobPostingsForEmployeeQuery(userId));

            return Ok(new ApiResponse<List<JobPostingDto>>("Job postings retrieved", jobPostings));
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<JobPostingDto>>> Create([FromBody] CreateJobPostingRequest request)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var jobPosting = await _mediator.Send(new CreateJobPostingCommand(userId, request));

            return Ok(new ApiResponse<JobPostingDto>("Job posting created", jobPosting));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<JobPostingDto>>> Get([FromRoute] long id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var jobPosting = await _mediator.Send(new GetJobPostingForEmployeeQuery(userId, id));

            return Ok(new ApiResponse<JobPostingDto>("Job posting retrieved", jobPosting));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse>> Update([FromRoute] long id, [FromBody] UpdateJobPostingRequest request)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            await _mediator.Send(new UpdateJobPostingCommand(userId, id, request));

            return Ok(new ApiResponse("Job posting updated"));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse>> Delete([FromRoute] long id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            await _mediator.Send(new DeleteJobPostingCommand(userId, id));

            return Ok(new ApiResponse("Job posting deleted"));
        }

        [HttpGet("export")]
        public async Task<IActionResult> Export()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var json = await _mediator.Send(new GetJobPostingsExportForEmployeeQuery(userId));

            return File(System.Text.Encoding.UTF8.GetBytes(json), "application/json", "job-postings.json");
        }

        [HttpPost("import")]
        public async Task<ActionResult<ApiResponse<List<JobPostingDto>>>> Import(IFormFile file)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (file is null || file.Length == 0)
            {
                return BadRequest(new ApiResponse("File is empty"));
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
                var created = await _mediator.Send(new CreateJobPostingCommand(userId, request));
                imported.Add(created);
            }

            return Ok(new ApiResponse<List<JobPostingDto>>($"{imported.Count} job posting(s) imported", imported));
        }
    }
}
