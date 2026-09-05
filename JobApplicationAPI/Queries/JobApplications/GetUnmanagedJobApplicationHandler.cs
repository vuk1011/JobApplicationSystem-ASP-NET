using Domain.Repositories;
using MediatR;

namespace JobApplicationAPI.Queries.JobApplications
{
    public class GetUnmanagedJobApplicationHandler : IRequestHandler<GetUnmanagedJobApplicationQuery, Unit>
    {
        private readonly IUnitOfWork _uow;

        public GetUnmanagedJobApplicationHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Unit> Handle(GetUnmanagedJobApplicationQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
