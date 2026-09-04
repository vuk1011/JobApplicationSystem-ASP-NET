using MediatR;

namespace JobApplicationAPI.Queries.Candidates
{
    public record GetCandidateQuery : IRequest<Unit>;
}
