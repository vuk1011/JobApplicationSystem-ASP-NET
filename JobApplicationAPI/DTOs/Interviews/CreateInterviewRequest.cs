namespace JobApplicationAPI.DTOs.Interviews
{
    public record CreateInterviewRequest
    {
        public string Title { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public DateTime TimeScheduled { get; init; }
        public long JobApplicationId { get; init; }
    }
}
