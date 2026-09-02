using JobApplicationAPI.DTOs.Offers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobApplicationAPI.Controllers.Candidates
{
    [Route("api/candidates/offers")]
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

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] long offerId, [FromBody] UpdateOfferRequest request)
        {
            return Ok();
        }
    }
}
