using MediatR;

namespace JobApplicationAPI.Commands.Candidates
{
    public record UpdateResumeCommand : IRequest<Unit>;
}
