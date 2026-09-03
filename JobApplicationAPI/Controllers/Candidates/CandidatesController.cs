using FluentValidation;
using JobApplicationAPI.DTOs;
using JobApplicationAPI.DTOs.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobApplicationAPI.Controllers.Candidates
{
    [ApiController]
    [Route("api/candidates/me")]
    [Authorize(Roles = "Candidate")]
    public class CandidatesController : ControllerBase
    {
        private readonly IValidator<UpdateCandidateRequest> _updateCandidateValidator;

        public CandidatesController(IValidator<UpdateCandidateRequest> updateCandidateValidator)
        {
            _updateCandidateValidator = updateCandidateValidator;
        }

        [HttpGet("resume")]
        public async Task<FileResult> GetResume()
        {
            return File(Array.Empty<byte>(), "application/pdf", "resume.pdf");
        }

        [HttpPut("resume")]
        public async Task<ActionResult<ApiResponse>> UploadResume(IFormFile file)
        {
            return Ok();
        }

        [HttpDelete("resume")]
        public async Task<ActionResult<ApiResponse>> DeleteResume()
        {
            return Ok();
        }

        [HttpGet("profile")]
        public ActionResult<ApiResponse<CandidateDto>> GetProfile()
        {
            return Ok();
        }

        [HttpPut("profile")]
        public ActionResult<ApiResponse<CandidateDto>> UpdateProfile([FromBody] UpdateCandidateRequest request)
        {
            return Ok();
        }
    }
}
