using MediatR;

namespace JobApplicationAPI.Queries.Offers
{
    public class GetOffersByJobApplicationForEmployeeHandler : IRequestHandler<GetOffersByJobApplicationForEmployeeQuery, Unit>
    {
        public GetOffersByJobApplicationForEmployeeHandler()
        {
            
        }

        public async Task<Unit> Handle(GetOffersByJobApplicationForEmployeeQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
