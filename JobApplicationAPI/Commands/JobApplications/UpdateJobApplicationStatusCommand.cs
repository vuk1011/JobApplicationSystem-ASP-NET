using MediatR;

namespace JobApplicationAPI.Commands.JobApplications
{
    public record UpdateJobApplicationStatusCommand : IRequest<Unit>;
}
