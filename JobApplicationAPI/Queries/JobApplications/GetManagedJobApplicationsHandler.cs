using Domain.Repositories;
using MediatR;

namespace JobApplicationAPI.Queries.JobApplications
{
    public class GetManagedJobApplicationsHandler : IRequestHandler<GetManagedJobApplicationsQuery, Unit>
    {
        private readonly IUnitOfWork _uow;

        public GetManagedJobApplicationsHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Unit> Handle(GetManagedJobApplicationsQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
