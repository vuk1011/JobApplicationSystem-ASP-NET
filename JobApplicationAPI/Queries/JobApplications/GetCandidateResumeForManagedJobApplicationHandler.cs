using Domain.Repositories;
using MediatR;

namespace JobApplicationAPI.Queries.JobApplications
{
    public class GetCandidateResumeForManagedJobApplicationHandler : IRequestHandler<GetCandidateResumeForManagedJobApplicationQuery, Unit>
    {
        private readonly IUnitOfWork _uow;

        public GetCandidateResumeForManagedJobApplicationHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Unit> Handle(GetCandidateResumeForManagedJobApplicationQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
