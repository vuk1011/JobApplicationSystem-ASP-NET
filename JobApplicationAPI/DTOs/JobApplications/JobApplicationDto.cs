using Domain.Entities;
using JobApplicationAPI.DTOs.JobPostings;

namespace JobApplicationAPI.DTOs.JobApplications
{
    public record JobApplicationCandidateDto
    {
        public long Id { get; init; }
        public DateOnly DateOfSubmission { get; init; }
        public JobApplicationStatus Status { get; init; }
        public JobPostingDto JobPosting { get; init; } = null!;
    }

    public record JobApplicationEmployeeDto
    {
        public long Id { get; init; }
        public DateOnly DateOfSubmission { get; init; }
        public JobApplicationStatus Status { get; init; }
        public JobPostingDto JobPosting { get; init; } = null!;
        public long CandidateId { get; init; }
    }
}
