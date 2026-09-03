using Domain.Repositories;
using JobApplicationAPI.DTOs;
using JobApplicationAPI.DTOs.Interviews;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobApplicationAPI.Controllers.Candidates
{
    [ApiController]
    [Route("api/candidates/interviews")]
    [Authorize(Roles = "Candidate")]
    public class InterviewsController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        public InterviewsController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpGet]
        public ActionResult<ApiResponse<List<InterviewDto>>> GetAll([FromQuery] long jobApplicationId)
        {
            return Ok();
        }
    }
}
