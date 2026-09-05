using MediatR;

namespace JobApplicationAPI.Commands.JobPostings
{
    public record DeleteJobPostingCommand(string? UserId, long JobPostingId) : IRequest<Unit>;
}
