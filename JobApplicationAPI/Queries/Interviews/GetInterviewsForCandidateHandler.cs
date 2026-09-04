using MediatR;

namespace JobApplicationAPI.Queries.Interviews
{
    public class GetInterviewsForCandidateHandler : IRequestHandler<GetInterviewsForCandidateQuery, Unit>
    {
        public GetInterviewsForCandidateHandler()
        {
            
        }

        public async Task<Unit> Handle(GetInterviewsForCandidateQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
