using MediatR;

namespace JobApplicationAPI.Queries.JobPostings
{
    public class GetJobPostingsForEmployeeHandler : IRequestHandler<GetJobPostingsForEmployeeQuery, Unit>
    {
        public GetJobPostingsForEmployeeHandler()
        {
            
        }

        public async Task<Unit> Handle(GetJobPostingsForEmployeeQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
