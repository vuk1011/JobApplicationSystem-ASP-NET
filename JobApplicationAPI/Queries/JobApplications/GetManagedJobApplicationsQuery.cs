using MediatR;

namespace JobApplicationAPI.Queries.JobApplications
{
    public record GetManagedJobApplicationsQuery : IRequest<Unit>;
}
