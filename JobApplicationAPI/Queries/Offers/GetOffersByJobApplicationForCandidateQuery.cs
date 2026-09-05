using JobApplicationAPI.DTOs.Offers;
using MediatR;

namespace JobApplicationAPI.Queries.Offers
{
    public record GetOffersByJobApplicationForCandidateQuery(string? UserId, long JobApplicationId) : IRequest<List<OfferDto>>;
}
