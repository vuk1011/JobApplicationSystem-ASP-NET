namespace JobApplicationAPI.DTOs.Offers
{
    public class OfferDto
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool? Accepted { get; set; }
    }
}
