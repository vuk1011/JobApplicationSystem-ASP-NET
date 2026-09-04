using MediatR;

namespace JobApplicationAPI.Queries.JobApplications
{
    public record GetJobApplicationsForCandidateQuery : IRequest<Unit>;
}
