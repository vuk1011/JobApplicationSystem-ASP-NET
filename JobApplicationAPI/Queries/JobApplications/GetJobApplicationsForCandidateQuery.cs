using JobApplicationAPI.DTOs.JobApplications;
using MediatR;

namespace JobApplicationAPI.Queries.JobApplications
{
    public record GetJobApplicationsForCandidateQuery(string? UserId) : IRequest<List<JobApplicationCandidateDto>>;
}
