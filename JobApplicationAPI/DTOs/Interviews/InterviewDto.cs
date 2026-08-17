namespace JobApplicationAPI.DTOs.Interviews
{
    public class InterviewDto
    {
        public long Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime DateTimeScheduled { get; set; }
    }
}
