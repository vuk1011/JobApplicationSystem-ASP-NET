using MediatR;

namespace JobApplicationAPI.Commands.Candidates
{
    public record UpdateCandidateCommand : IRequest<Unit>;
}
