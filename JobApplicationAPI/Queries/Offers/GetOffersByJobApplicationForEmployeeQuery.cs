using MediatR;

namespace JobApplicationAPI.Queries.Offers
{
    public record GetOffersByJobApplicationForEmployeeQuery : IRequest<Unit>;
}
