using MediatR;

namespace JobApplicationAPI.Queries.JobPostings
{
    public record GetJobPostingsExportForEmployeeQuery : IRequest<Unit>;
}
