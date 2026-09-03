namespace JobApplicationAPI.DTOs.JobPostings
{
    public class UpdateJobPostingRequest
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateOnly DateOfExpiration { get; set; }
    }
}
