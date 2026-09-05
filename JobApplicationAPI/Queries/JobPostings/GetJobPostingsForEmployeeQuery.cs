using JobApplicationAPI.DTOs.JobPostings;
using MediatR;

namespace JobApplicationAPI.Queries.JobPostings
{
    public record GetJobPostingsForEmployeeQuery(string? UserId) : IRequest<List<JobPostingDto>>;
}
