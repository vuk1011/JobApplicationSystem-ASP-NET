namespace Domain.Entities
{
    public class JobPosting
    {
        public long Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateOnly DateOfPublishing { get; set; }
        public DateOnly DateOfExpiration { get; set; }
        public long CompanyId { get; set; }
        public Company Company { get; set; } = null!;

        public bool IsClosed => DateOfExpiration < DateOnly.FromDateTime(DateTime.Today);
        public JobPostingStatus Status => IsClosed ? JobPostingStatus.Closed : JobPostingStatus.Published;
    }

    public enum JobPostingStatus
    {
        Published,
        Closed,
    }
}
