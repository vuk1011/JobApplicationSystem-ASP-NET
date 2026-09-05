using Domain.Repositories;
using MediatR;

namespace JobApplicationAPI.Queries.JobPostings
{
    public class GetJobPostingsForEmployeeHandler : IRequestHandler<GetJobPostingsForEmployeeQuery, Unit>
    {
        private readonly IUnitOfWork _uow;

        public GetJobPostingsForEmployeeHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Unit> Handle(GetJobPostingsForEmployeeQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
