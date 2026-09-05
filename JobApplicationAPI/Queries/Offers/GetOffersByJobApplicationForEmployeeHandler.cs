using Domain.Repositories;
using MediatR;

namespace JobApplicationAPI.Queries.Offers
{
    public class GetOffersByJobApplicationForEmployeeHandler : IRequestHandler<GetOffersByJobApplicationForEmployeeQuery, Unit>
    {
        private readonly IUnitOfWork _uow;

        public GetOffersByJobApplicationForEmployeeHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Unit> Handle(GetOffersByJobApplicationForEmployeeQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
