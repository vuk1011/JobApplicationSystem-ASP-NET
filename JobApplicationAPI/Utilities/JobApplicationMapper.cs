using Domain.Entities;
using JobApplicationAPI.DTOs.JobApplications;

namespace JobApplicationAPI.Utilities
{
    public static class JobApplicationMapper
    {
        public static JobApplicationCandidateDto ToCandidateDto(JobApplication application) => new()
        {
            Id = application.Id,
            DateOfSubmission = application.DateOfSubmission,
            Status = application.Status,
            JobPosting = JobPostingMapper.ToDto(application.JobPosting),
        };

        public static JobApplicationEmployeeDto ToEmployeeDto(JobApplication application) => new()
        {
            Id = application.Id,
            DateOfSubmission = application.DateOfSubmission,
            Status = application.Status,
            JobPosting = JobPostingMapper.ToDto(application.JobPosting),
            CandidateId = application.CandidateId,
        };
    }
}
