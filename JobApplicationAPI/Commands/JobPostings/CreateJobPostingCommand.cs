using MediatR;

namespace JobApplicationAPI.Commands.JobPostings
{
    public record CreateJobPostingCommand : IRequest<Unit>;
}
