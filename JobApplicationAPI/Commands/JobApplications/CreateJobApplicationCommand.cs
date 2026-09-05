using JobApplicationAPI.DTOs.JobApplications;
using MediatR;

namespace JobApplicationAPI.Commands.JobApplications
{
    public record CreateJobApplicationCommand(string? UserId, SubmitJobApplicationRequest Request) : IRequest<Unit>;
}
