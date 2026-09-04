using MediatR;

namespace JobApplicationAPI.Queries.JobApplications
{
    public record GetJobApplicationForCandidateQuery : IRequest<Unit>;
}
