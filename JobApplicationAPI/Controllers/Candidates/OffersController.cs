using JobApplicationAPI.DTOs;
using JobApplicationAPI.DTOs.Offers;
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
        public ActionResult<ApiResponse<List<OfferDto>>> GetAll([FromQuery] long jobApplicationId)
        {
            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult<ApiResponse> Update([FromRoute] long offerId, [FromBody] UpdateOfferRequest request)
        {
            return Ok();
        }
    }
}
