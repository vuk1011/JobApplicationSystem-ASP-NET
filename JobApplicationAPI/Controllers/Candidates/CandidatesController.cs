using JobApplicationAPI.Commands.Candidates;
using JobApplicationAPI.DTOs;
using JobApplicationAPI.DTOs.Users;
using JobApplicationAPI.Queries.Candidates;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JobApplicationAPI.Controllers.Candidates
{
    [ApiController]
    [Route("api/candidates/me")]
    [Authorize(Roles = "Candidate")]
    public class CandidatesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CandidatesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("resume")]
        public async Task<IActionResult> GetResume()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var resumeBytes = await _mediator.Send(new GetResumeQuery(userId));

            Response.Headers.ContentDisposition = "inline; filename=\"resume.pdf\"";
            return File(resumeBytes, "application/pdf");
        }

        [HttpPut("resume")]
        public async Task<ActionResult<ApiResponse>> UploadResume(IFormFile file)
        {
            if (file is null || file.ContentType != "application/pdf")
            {
                return BadRequest(new ApiResponse("Only PDF resumes are allowed"));
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            await _mediator.Send(new UpdateResumeCommand(userId, file.OpenReadStream()));

            return Ok(new ApiResponse("Resume uploaded successfully"));
        }

        [HttpDelete("resume")]
        public async Task<ActionResult<ApiResponse>> DeleteResume()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            await _mediator.Send(new DeleteResumeCommand(userId));

            return Ok(new ApiResponse("Resume deleted successfully"));
        }

        [HttpGet("profile")]
        public async Task<ActionResult<ApiResponse<CandidateDto>>> GetProfile()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var candidateDto = await _mediator.Send(new GetCandidateQuery(userId));

            return Ok(new ApiResponse<CandidateDto>("Profile information retrieved successfully", candidateDto));
        }

        [HttpPut("profile")]
        public async Task<ActionResult<ApiResponse<CandidateDto>>> UpdateProfile([FromBody] UpdateCandidateRequest request)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var candidateDto = await _mediator.Send(new UpdateCandidateCommand(userId, request));

            return Ok(new ApiResponse<CandidateDto>("Profile information updated successfully", candidateDto));
        }
    }
}
