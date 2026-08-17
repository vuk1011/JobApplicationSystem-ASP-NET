namespace JobApplicationAPI.DTOs.JobPostings
{
    public class JobPostingDto
    {
        public long Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateOnly DatePublished { get; set; }
        public DateOnly DateExpires { get; set; }
        public bool IsClosed { get; set; }
        public string CompanyName { get; set; } = string.Empty;
    }
}
