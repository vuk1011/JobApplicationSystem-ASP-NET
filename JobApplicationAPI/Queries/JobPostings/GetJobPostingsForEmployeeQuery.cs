using MediatR;

namespace JobApplicationAPI.Queries.JobPostings
{
    public record GetJobPostingsForEmployeeQuery : IRequest<Unit>;
}
