using Domain.Entities;

namespace JobApplicationAPI.DTOs.JobApplications
{
    public class UpdateJobApplicationStatusRequest
    {
        public JobApplicationStatus Status { get; set; }
    }
}
