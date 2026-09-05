using MediatR;

namespace JobApplicationAPI.Queries.JobPostings
{
    public record GetJobPostingsExportForEmployeeQuery(string? UserId) : IRequest<string>;
}
