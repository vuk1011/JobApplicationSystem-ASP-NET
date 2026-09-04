using MediatR;

namespace JobApplicationAPI.Commands.JobPostings
{
    public record UpdateJobPostingCommand : IRequest<Unit>;
}
