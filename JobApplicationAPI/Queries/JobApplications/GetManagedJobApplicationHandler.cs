using Domain.Repositories;
using MediatR;

namespace JobApplicationAPI.Queries.JobApplications
{
    public class GetManagedJobApplicationHandler : IRequestHandler<GetManagedJobApplicationQuery, Unit>
    {
        private readonly IUnitOfWork _uow;

        public GetManagedJobApplicationHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Unit> Handle(GetManagedJobApplicationQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
