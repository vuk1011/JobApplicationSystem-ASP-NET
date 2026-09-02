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
        public IActionResult GetAll([FromQuery] long jobApplicationId)
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateOfferRequest request)
        {
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] long id)
        {
            return Ok();
        }
    }
}
