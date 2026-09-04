using MediatR;

namespace JobApplicationAPI.Queries.JobPostings
{
    public class GetJobPostingsPublishedHandler : IRequestHandler<GetJobPostingsPublishedQuery, Unit>
    {
        public GetJobPostingsPublishedHandler()
        {
            
        }

        public async Task<Unit> Handle(GetJobPostingsPublishedQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
