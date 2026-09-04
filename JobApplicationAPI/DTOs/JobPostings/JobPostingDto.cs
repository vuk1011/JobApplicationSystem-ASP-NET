using Domain.Entities;

namespace JobApplicationAPI.DTOs.JobPostings
{
    public record JobPostingDto
    {
        public long Id { get; init; }
        public string Title { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public DateOnly DateOfPublishing { get; init; }
        public DateOnly DateOfExpiration { get; init; }
        public JobPostingStatus Status { get; init; }
        public bool IsClosed { get; init; }
        public string CompanyName { get; init; } = string.Empty;
    }
}
