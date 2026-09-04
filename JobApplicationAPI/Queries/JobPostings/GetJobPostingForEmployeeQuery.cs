using MediatR;

namespace JobApplicationAPI.Queries.JobPostings
{
    public record GetJobPostingForEmployeeQuery : IRequest<Unit>;
}
