using MediatR;

namespace JobApplicationAPI.Queries.Offers
{
    public class GetOffersByJobApplicationForCandidateHandler : IRequestHandler<GetOffersByJobApplicationForCandidateQuery, Unit>
    {
        public GetOffersByJobApplicationForCandidateHandler()
        {
            
        }

        public async Task<Unit> Handle(GetOffersByJobApplicationForCandidateQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
