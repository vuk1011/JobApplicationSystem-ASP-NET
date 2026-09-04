namespace JobApplicationAPI.DTOs.Offers
{
    public record UpdateOfferRequest
    {
        public bool Accepted { get; init; }
    }
}
