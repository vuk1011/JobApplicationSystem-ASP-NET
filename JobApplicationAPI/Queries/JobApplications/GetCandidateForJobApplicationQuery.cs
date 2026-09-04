using MediatR;

namespace JobApplicationAPI.Queries.JobApplications
{
    public record GetCandidateForJobApplicationQuery : IRequest<Unit>;
}
