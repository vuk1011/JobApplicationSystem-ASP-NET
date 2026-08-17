using Domain.Entities;
using JobApplicationAPI.DTOs.JobPostings;

namespace JobApplicationAPI.DTOs.JobApplications
{
    public class JobApplicationCandidateDto
    {
        public long Id { get; set; }
        public DateOnly DateSubmitted { get; set; }
        public JobApplicationStatus Status { get; set; }
        public JobPostingDto JobPosting { get; set; }
    }

    public class JobApplicationEmployeeDto
    {
        public long Id { get; set; }
        public DateOnly DateSubmitted { get; set; }
        public JobApplicationStatus Status { get; set; }
        public JobPostingDto JobPosting { get; set; }
        public long CandidateId { get; set; }
    }
}
