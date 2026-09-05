using MediatR;

namespace JobApplicationAPI.Commands.JobApplications
{
    public record DeleteJobApplicationCommand(string? UserId, long JobApplicationId) : IRequest<Unit>;
}
