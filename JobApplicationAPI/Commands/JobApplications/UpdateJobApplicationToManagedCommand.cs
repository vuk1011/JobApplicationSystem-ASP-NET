using MediatR;

namespace JobApplicationAPI.Commands.JobApplications
{
    public record UpdateJobApplicationToManagedCommand : IRequest<Unit>;
}
