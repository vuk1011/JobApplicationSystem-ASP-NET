using JobApplicationAPI.DTOs.JobApplications;
using MediatR;

namespace JobApplicationAPI.Queries.JobApplications
{
    public record GetUnmanagedJobApplicationsByJobPostingQuery(string? UserId, long JobPostingId) : IRequest<List<JobApplicationEmployeeDto>>;
}
