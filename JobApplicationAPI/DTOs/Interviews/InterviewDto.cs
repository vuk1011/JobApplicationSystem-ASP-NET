namespace JobApplicationAPI.DTOs.Interviews
{
    public record InterviewDto
    {
        public long Id { get; init; }
        public string Title { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public DateTime TimeScheduled { get; init; }
    }
}
