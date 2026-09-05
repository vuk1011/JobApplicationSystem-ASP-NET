using JobApplicationAPI.DTOs.JobApplications;
using MediatR;

namespace JobApplicationAPI.Queries.JobApplications
{
    public record GetJobApplicationForCandidateQuery(string? UserId, long JobApplicationId) : IRequest<JobApplicationCandidateDto>;
}
