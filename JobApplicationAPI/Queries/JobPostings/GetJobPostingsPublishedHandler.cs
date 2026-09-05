using Domain.Repositories;
using MediatR;

namespace JobApplicationAPI.Queries.JobPostings
{
    public class GetJobPostingsPublishedHandler : IRequestHandler<GetJobPostingsPublishedQuery, Unit>
    {
        private readonly IUnitOfWork _uow;

        public GetJobPostingsPublishedHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Unit> Handle(GetJobPostingsPublishedQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
