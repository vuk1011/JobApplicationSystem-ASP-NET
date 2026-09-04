using MediatR;

namespace JobApplicationAPI.Queries.JobApplications
{
    public record GetManagedJobApplicationQuery : IRequest<Unit>;
}
