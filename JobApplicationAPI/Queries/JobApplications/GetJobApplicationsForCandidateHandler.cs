using Domain.Repositories;
using MediatR;

namespace JobApplicationAPI.Queries.JobApplications
{
    public class GetJobApplicationsForCandidateHandler : IRequestHandler<GetJobApplicationsForCandidateQuery, Unit>
    {
        private readonly IUnitOfWork _uow;

        public GetJobApplicationsForCandidateHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Unit> Handle(GetJobApplicationsForCandidateQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
