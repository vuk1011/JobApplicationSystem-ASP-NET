using JobApplicationAPI.DTOs.JobPostings;
using MediatR;

namespace JobApplicationAPI.Commands.JobPostings
{
    public record UpdateJobPostingCommand(string? UserId, long JobPostingId, UpdateJobPostingRequest Request) : IRequest<Unit>;
}
