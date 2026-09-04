namespace JobApplicationAPI.DTOs.Users
{
    public record LoginRequest
    {
        public string Email { get; init; } = string.Empty;
        public string Password { get; init; } = string.Empty;
    }

    public record LoginSuccessResponse
    {
        public string Jwt { get; init; } = string.Empty;
        public string FirstName { get; init; } = string.Empty;
    }
}
