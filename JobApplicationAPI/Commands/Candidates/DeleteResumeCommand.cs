using MediatR;

namespace JobApplicationAPI.Commands.Candidates
{
    public record DeleteResumeCommand(string? UserId) : IRequest<Unit>;
}
