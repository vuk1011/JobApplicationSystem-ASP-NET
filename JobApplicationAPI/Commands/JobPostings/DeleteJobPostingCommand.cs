using MediatR;

namespace JobApplicationAPI.Commands.JobPostings
{
    public record DeleteJobPostingCommand : IRequest<Unit>;
}
