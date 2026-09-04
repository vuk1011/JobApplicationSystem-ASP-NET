using MediatR;

namespace JobApplicationAPI.Commands.Candidates
{
    public record DeleteResumeCommand : IRequest<Unit>;
}
