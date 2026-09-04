using Domain.Entities;
using JobApplicationAPI.DTOs.JobPostings;

namespace JobApplicationAPI.Utilities
{
    public static class JobPostingMapper
    {
        public static JobPostingDto ToDto(JobPosting jobPosting) => new()
        {
            Id = jobPosting.Id,
            Title = jobPosting.Title,
            Description = jobPosting.Description,
            DateOfPublishing = jobPosting.DateOfPublishing,
            DateOfExpiration = jobPosting.DateOfExpiration,
            Status = jobPosting.Status,
            IsClosed = jobPosting.IsClosed,
            CompanyName = jobPosting.Company?.Name ?? string.Empty,
        };
    }
}
