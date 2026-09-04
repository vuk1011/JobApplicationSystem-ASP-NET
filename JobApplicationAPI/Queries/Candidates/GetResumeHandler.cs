using MediatR;

namespace JobApplicationAPI.Queries.Candidates
{
    public class GetResumeHandler : IRequestHandler<GetResumeQuery, Unit>
    {
        public GetResumeHandler()
        {

        }

        public async Task<Unit> Handle(GetResumeQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
