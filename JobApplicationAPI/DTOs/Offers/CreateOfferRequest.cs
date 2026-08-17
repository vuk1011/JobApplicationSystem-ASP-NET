namespace JobApplicationAPI.DTOs.Offers
{
    public class CreateOfferRequest
    {
        public string Name { get; set; } = string.Empty;
        public long JobApplicationId { get; set; }
    }
}
