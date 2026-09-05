using Domain.Repositories;
using MediatR;

namespace JobApplicationAPI.Queries.JobApplications
{
    public class GetUnmanagedJobApplicationsByJobPostingHandler : IRequestHandler<GetUnmanagedJobApplicationsByJobPostingQuery, Unit>
    {
        private readonly IUnitOfWork _uow;

        public GetUnmanagedJobApplicationsByJobPostingHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Unit> Handle(GetUnmanagedJobApplicationsByJobPostingQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
