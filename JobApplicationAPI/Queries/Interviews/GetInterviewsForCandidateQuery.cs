using MediatR;

namespace JobApplicationAPI.Queries.Interviews
{
    public record GetInterviewsForCandidateQuery : IRequest<Unit>;
}
