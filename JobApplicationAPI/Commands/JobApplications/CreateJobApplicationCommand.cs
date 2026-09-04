using MediatR;

namespace JobApplicationAPI.Commands.JobApplications
{
    public record CreateJobApplicationCommand : IRequest<Unit>;
}
