using MediatR;

namespace JobApplicationAPI.Queries.JobPostings
{
    public class GetJobPostingsExportForEmployeeHandler : IRequestHandler<GetJobPostingsExportForEmployeeQuery, Unit>
    {
        public GetJobPostingsExportForEmployeeHandler()
        {
            
        }

        public async Task<Unit> Handle(GetJobPostingsExportForEmployeeQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
