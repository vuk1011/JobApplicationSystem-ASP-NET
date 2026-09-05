using Domain.Repositories;
using MediatR;

namespace JobApplicationAPI.Queries.JobApplications
{
    public class GetJobApplicationForCandidateHandler : IRequestHandler<GetJobApplicationForCandidateQuery, Unit>
    {
        private readonly IUnitOfWork _uow;

        public GetJobApplicationForCandidateHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Unit> Handle(GetJobApplicationForCandidateQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
