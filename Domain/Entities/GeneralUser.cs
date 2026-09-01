namespace Domain.Entities
{
    public abstract class GeneralUser
    {
        public long Id { get; set; }
        public string AppUserId { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public Sex Sex { get; set; }
        public string Address { get; set; } = string.Empty;
    }

    public enum Sex
    {
        Male,
        Female,
    }
}
