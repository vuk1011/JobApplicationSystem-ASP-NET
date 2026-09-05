using Domain.Repositories;
using MediatR;

namespace JobApplicationAPI.Queries.Offers
{
    public class GetOffersByJobApplicationForCandidateHandler : IRequestHandler<GetOffersByJobApplicationForCandidateQuery, Unit>
    {
        private readonly IUnitOfWork _uow;

        public GetOffersByJobApplicationForCandidateHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Unit> Handle(GetOffersByJobApplicationForCandidateQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
