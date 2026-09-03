using Domain.Entities;

namespace JobApplicationAPI.DTOs.JobPostings
{
    public class JobPostingDto
    {
        public long Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateOnly DateOfPublishing { get; set; }
        public DateOnly DateOfExpiration { get; set; }
        public JobPostingStatus Status { get; set; }
        public bool IsClosed { get; set; }
        public string CompanyName { get; set; } = string.Empty;
    }
}
