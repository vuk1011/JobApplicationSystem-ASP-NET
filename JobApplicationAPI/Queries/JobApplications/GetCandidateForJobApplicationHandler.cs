using MediatR;

namespace JobApplicationAPI.Queries.JobApplications
{
    public class GetCandidateForJobApplicationHandler : IRequestHandler<GetCandidateForJobApplicationQuery, Unit>
    {
        public GetCandidateForJobApplicationHandler()
        {
            
        }

        public async Task<Unit> Handle(GetCandidateForJobApplicationQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
