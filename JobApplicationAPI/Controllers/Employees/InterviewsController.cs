using JobApplicationAPI.Commands.Interviews;
using JobApplicationAPI.DTOs;
using JobApplicationAPI.DTOs.Interviews;
using JobApplicationAPI.Queries.Interviews;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JobApplicationAPI.Controllers.Employees
{
    [ApiController]
    [Route("api/employees/interviews")]
    [Authorize(Roles = "Employee")]
    public class InterviewsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public InterviewsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<InterviewDto>>>> GetAll([FromQuery] long jobApplicationId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var interviews = await _mediator.Send(new GetInterviewsForEmployeeQuery(userId, jobApplicationId));

            return Ok(new ApiResponse<List<InterviewDto>>("Interviews retrieved", interviews));
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse>> Schedule([FromBody] CreateInterviewRequest request)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            await _mediator.Send(new CreateInterviewCommand(userId, request));

            return Ok(new ApiResponse("Interview scheduled"));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse>> Delete([FromRoute] long id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            await _mediator.Send(new DeleteInterviewCommand(userId, id));

            return Ok(new ApiResponse("Interview cancelled"));
        }
    }
}
