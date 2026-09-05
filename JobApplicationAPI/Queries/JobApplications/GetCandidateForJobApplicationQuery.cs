using JobApplicationAPI.DTOs.Users;
using MediatR;

namespace JobApplicationAPI.Queries.JobApplications
{
    public record GetCandidateForJobApplicationQuery(string? UserId, long JobApplicationId) : IRequest<CandidateDto>;
}
