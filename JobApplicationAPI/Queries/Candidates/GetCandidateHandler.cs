using MediatR;

namespace JobApplicationAPI.Queries.Candidates
{
    public class GetCandidateHandler : IRequestHandler<GetCandidateQuery, Unit>
    {
        public GetCandidateHandler()
        {
            
        }

        public async Task<Unit> Handle(GetCandidateQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
}
