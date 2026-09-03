using FluentValidation;
using JobApplicationAPI.DTOs;
using JobApplicationAPI.DTOs.Offers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobApplicationAPI.Controllers.Employees
{
    [ApiController]
    [Route("api/employees/offers")]
    [Authorize(Roles = "Employee")]
    public class OffersController : ControllerBase
    {
        private readonly IValidator<CreateOfferRequest> _createValidator;

        public OffersController(IValidator<CreateOfferRequest> createValidator)
        {
            _createValidator = createValidator;
        }

        [HttpGet]
        public ActionResult<ApiResponse<List<OfferDto>>> GetAll([FromQuery] long jobApplicationId)
        {
            return Ok();
        }

        [HttpPost]
        public ActionResult<ApiResponse> Create([FromBody] CreateOfferRequest request)
        {
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult<ApiResponse> Delete([FromRoute] long id)
        {
            return Ok();
        }
    }
}
