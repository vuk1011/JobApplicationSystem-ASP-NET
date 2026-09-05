using Domain.Entities;
using JobApplicationAPI.DTOs.Offers;

namespace JobApplicationAPI.Utilities
{
    public static class OfferMapper
    {
        public static OfferDto ToDto(Offer offer) => new()
        {
            Id = offer.Id,
            Name = offer.Name,
            Accepted = offer.Accepted
        };
    }
}
