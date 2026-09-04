using Domain.Entities;

namespace JobApplicationAPI.DTOs.Users
{
    public record CandidateDto
    {
        public long Id { get; init; }
        public string FirstName { get; init; } = string.Empty;
        public string LastName { get; init; } = string.Empty;
        public Sex Sex { get; init; }
        public string Address { get; init; } = string.Empty;
        public string Email { get; init; } = string.Empty;
        public string Phone { get; init; } = string.Empty;
    }
}
