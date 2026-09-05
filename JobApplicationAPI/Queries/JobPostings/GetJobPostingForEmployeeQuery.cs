using JobApplicationAPI.DTOs.JobPostings;
using MediatR;

namespace JobApplicationAPI.Queries.JobPostings
{
    public record GetJobPostingForEmployeeQuery(string? UserId, long JobPostingId) : IRequest<JobPostingDto>;
}
