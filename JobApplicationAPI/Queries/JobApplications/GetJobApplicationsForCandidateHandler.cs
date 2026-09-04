using MediatR;

namespace JobApplicationAPI.Queries.JobApplications
{
    public class GetJobApplicationsForCandidateHandler : IRequestHandler<GetJobApplicationsForCandidateQuery, Unit>
    {
        public GetJobApplicationsForCandidateHandler()
        {
            
        }

        public async Task<Unit> Handle(GetJobApplicationsForCandidateQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
