using Domain.Repositories;
using FluentValidation;
using JobApplicationAPI.DTOs;
using JobApplicationAPI.DTOs.Interviews;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobApplicationAPI.Controllers.Employees
{
    [ApiController]
    [Route("api/employees/interviews")]
    [Authorize(Roles = "Employee")]
    public class InterviewsController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        private readonly IValidator<CreateInterviewRequest> _createValidator;

        public InterviewsController(IUnitOfWork uow, IValidator<CreateInterviewRequest> createValidator)
        {
            _uow = uow;
            _createValidator = createValidator;
        }

        [HttpGet]
        public ActionResult<ApiResponse<List<InterviewDto>>> GetAll([FromQuery] long jobApplicationId)
        {
            return Ok();
        }

        [HttpPost]
        public ActionResult<ApiResponse> Schedule([FromBody] CreateInterviewRequest request)
        {
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult<ApiResponse> Cancel([FromRoute] long interviewId)
        {
            return Ok();
        }
    }
}
