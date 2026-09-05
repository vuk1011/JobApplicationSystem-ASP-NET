using MediatR;

namespace JobApplicationAPI.Commands.JobPostings
{
    public record ImportJobPostingsCommand : IRequest<Unit>;
}
