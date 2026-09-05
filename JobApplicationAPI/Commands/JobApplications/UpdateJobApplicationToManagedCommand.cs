using JobApplicationAPI.DTOs.JobApplications;
using MediatR;

namespace JobApplicationAPI.Commands.JobApplications
{
    public record UpdateJobApplicationToManagedCommand(string? UserId, ManageJobApplicationRequest Request) : IRequest<Unit>;
}
