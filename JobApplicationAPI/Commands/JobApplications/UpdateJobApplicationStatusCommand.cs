using JobApplicationAPI.DTOs.JobApplications;
using MediatR;

namespace JobApplicationAPI.Commands.JobApplications
{
    public record UpdateJobApplicationStatusCommand(string? UserId, long JobApplicationId, UpdateJobApplicationStatusRequest Request) : IRequest<Unit>;
}
