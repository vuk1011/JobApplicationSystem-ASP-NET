using JobApplicationAPI.DTOs.Offers;
using MediatR;

namespace JobApplicationAPI.Queries.Offers
{
    public record GetOffersByJobApplicationForEmployeeQuery(string? UserId, long JobApplicationId) : IRequest<List<OfferDto>>;
}
