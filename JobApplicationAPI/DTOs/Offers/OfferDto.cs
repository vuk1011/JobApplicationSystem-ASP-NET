namespace JobApplicationAPI.DTOs.Offers
{
    public record OfferDto
    {
        public long Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public bool? Accepted { get; init; }
    }
}
