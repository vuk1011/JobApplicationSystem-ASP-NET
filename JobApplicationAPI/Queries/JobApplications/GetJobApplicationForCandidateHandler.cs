using MediatR;

namespace JobApplicationAPI.Queries.JobApplications
{
    public class GetJobApplicationForCandidateHandler : IRequestHandler<GetJobApplicationForCandidateQuery, Unit>
    {
        public GetJobApplicationForCandidateHandler()
        {
            
        }

        public async Task<Unit> Handle(GetJobApplicationForCandidateQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
