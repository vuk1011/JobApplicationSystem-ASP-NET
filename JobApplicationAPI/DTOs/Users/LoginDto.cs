namespace JobApplicationAPI.DTOs.Users
{
    public class LoginRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class LoginSuccessResponse
    {
        public string Jwt { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
    }
}
