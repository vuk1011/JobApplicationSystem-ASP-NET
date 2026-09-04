using MediatR;

namespace JobApplicationAPI.Queries.Candidates
{
    public record GetResumeQuery : IRequest<Unit>;
}
