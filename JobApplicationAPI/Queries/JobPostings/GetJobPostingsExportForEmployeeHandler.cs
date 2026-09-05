using Domain.Repositories;
using MediatR;

namespace JobApplicationAPI.Queries.JobPostings
{
    public class GetJobPostingsExportForEmployeeHandler : IRequestHandler<GetJobPostingsExportForEmployeeQuery, Unit>
    {
        private readonly IUnitOfWork _uow;

        public GetJobPostingsExportForEmployeeHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Unit> Handle(GetJobPostingsExportForEmployeeQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
