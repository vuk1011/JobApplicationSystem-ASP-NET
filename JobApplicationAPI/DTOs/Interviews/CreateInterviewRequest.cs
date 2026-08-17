namespace JobApplicationAPI.DTOs.Interviews
{
    public class CreateInterviewRequest
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime DateTimeScheduled { get; set; }
        public long JobApplicationId { get; set; }
    }
}
