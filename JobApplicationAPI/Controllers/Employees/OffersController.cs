using JobApplicationAPI.DTOs;
using JobApplicationAPI.DTOs.Offers;
using Microsoft.AspNetCore.Mvc;

namespace JobApplicationAPI.Controllers.Employees
{
    [Route("api/employees/offers")]
    [ApiController]
    public class OffersController : ControllerBase
    {
        public OffersController()
        {

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
