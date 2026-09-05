using MediatR;

namespace JobApplicationAPI.Queries.JobApplications
{
    public record GetCandidateResumeForManagedJobApplicationQuery(string? UserId, long JobApplicationId) : IRequest<byte[]>;
}
