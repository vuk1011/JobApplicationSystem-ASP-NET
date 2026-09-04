using MediatR;

namespace JobApplicationAPI.Queries.JobPostings
{
    public class GetJobPostingForEmployeeHandler : IRequestHandler<GetJobPostingForEmployeeQuery, Unit>
    {
        public GetJobPostingForEmployeeHandler()
        {
            
        }

        public async Task<Unit> Handle(GetJobPostingForEmployeeQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
