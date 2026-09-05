using MediatR;

namespace JobApplicationAPI.Queries.JobApplications
{
    public record GetCandidateResumeForManagedJobApplicationQuery : IRequest<Unit>;
}
