using MediatR;

namespace JobApplicationAPI.Queries.JobApplications
{
    public record GetUnmanagedJobApplicationsByJobPostingQuery : IRequest<Unit>;
}
