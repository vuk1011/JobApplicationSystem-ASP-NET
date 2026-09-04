namespace JobApplicationAPI.DTOs.Offers
{
    public record CreateOfferRequest
    {
        public string Name { get; init; } = string.Empty;
        public long JobApplicationId { get; init; }
    }
}
