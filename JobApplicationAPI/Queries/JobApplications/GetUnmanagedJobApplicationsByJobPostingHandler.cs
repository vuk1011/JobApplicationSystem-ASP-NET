using MediatR;

namespace JobApplicationAPI.Queries.JobApplications
{
    public class GetUnmanagedJobApplicationsByJobPostingHandler : IRequestHandler<GetUnmanagedJobApplicationsByJobPostingQuery, Unit>
    {
        public GetUnmanagedJobApplicationsByJobPostingHandler()
        {
            
        }

        public async Task<Unit> Handle(GetUnmanagedJobApplicationsByJobPostingQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
