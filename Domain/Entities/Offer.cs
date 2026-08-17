namespace Domain.Entities
{
    public class Offer
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool? Accepted { get; set; }
        public long JobApplicationId { get; set; }
        public JobApplication JobApplication { get; set; }
    }
}
