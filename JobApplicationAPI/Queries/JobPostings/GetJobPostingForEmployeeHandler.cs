using Domain.Repositories;
using MediatR;

namespace JobApplicationAPI.Queries.JobPostings
{
    public class GetJobPostingForEmployeeHandler : IRequestHandler<GetJobPostingForEmployeeQuery, Unit>
    {
        private readonly IUnitOfWork _uow;

        public GetJobPostingForEmployeeHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Unit> Handle(GetJobPostingForEmployeeQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
