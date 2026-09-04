namespace JobApplicationAPI.DTOs.JobPostings
{
    public record UpdateJobPostingRequest
    {
        public string Title { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public DateOnly DateOfExpiration { get; init; }
    }
}
