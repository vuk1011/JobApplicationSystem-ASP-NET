using MediatR;

namespace JobApplicationAPI.Commands.Candidates
{
    public record UpdateResumeCommand(string? UserId, Stream FileStream) : IRequest<Unit>;
}
