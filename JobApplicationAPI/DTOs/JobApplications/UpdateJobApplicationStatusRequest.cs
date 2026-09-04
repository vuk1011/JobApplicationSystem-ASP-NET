using Domain.Entities;

namespace JobApplicationAPI.DTOs.JobApplications
{
    public record UpdateJobApplicationStatusRequest
    {
        public JobApplicationStatus Status { get; init; }
    }
}
