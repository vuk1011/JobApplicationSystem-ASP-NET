using MediatR;

namespace JobApplicationAPI.Commands.JobApplications
{
    public record DeleteJobApplicationCommand : IRequest<Unit>;
}
