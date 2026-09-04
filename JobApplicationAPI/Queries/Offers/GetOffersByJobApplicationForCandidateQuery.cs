using MediatR;

namespace JobApplicationAPI.Queries.Offers
{
    public record GetOffersByJobApplicationForCandidateQuery : IRequest<Unit>;
}
