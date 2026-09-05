using JobApplicationAPI.DTOs.JobPostings;
using MediatR;

namespace JobApplicationAPI.Commands.JobPostings
{
    public record CreateJobPostingCommand(string? UserId, CreateJobPostingRequest Request) : IRequest<JobPostingDto>;
}
