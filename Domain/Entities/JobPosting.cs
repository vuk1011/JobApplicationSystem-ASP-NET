namespace Domain.Entities
{
    public class JobPosting
    {
        public long Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateOnly DatePublished { get; set; }
        public DateOnly DateExpires { get; set; }
        public long CompanyId { get; set; }
        public Company Company { get; set; }

        public bool IsClosed => DateExpires < DateOnly.FromDateTime(DateTime.Today);
    }
}
