using JobApplicationAPI.DTOs.JobPostings;
using MediatR;

namespace JobApplicationAPI.Queries.JobPostings
{
    public record GetJobPostingsPublishedQuery(string? UserId) : IRequest<List<JobPostingDto>>;
}
