using MediatR;

namespace JobApplicationAPI.Queries.JobApplications
{
    public record GetUnmanagedJobApplicationQuery : IRequest<Unit>;
}
