using Domain.Entities;

namespace JobApplicationAPI.DTOs.Users
{
    public record RegisterCandidateRequest
    {
        public string FirstName { get; init; } = string.Empty;
        public string LastName { get; init; } = string.Empty;
        public Sex Sex { get; init; }
        public string Phone { get; init; } = string.Empty;
        public string Address { get; init; } = string.Empty;
        public string Email { get; init; } = string.Empty;
        public string Password { get; init; } = string.Empty;
    }
}
