namespace Domain.Entities
{
    public class Interview
    {
        public long Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime DateTimeScheduled { get; set; }
        public long JobApplicationId { get; set; }
        public JobApplication JobApplication { get; set; }
    }
}
