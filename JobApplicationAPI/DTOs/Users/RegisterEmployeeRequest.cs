using Domain.Entities;

namespace JobApplicationAPI.DTOs.Users
{
    public class RegisterEmployeeRequest
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public Sex Sex { get; set; }
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string NationalId { get; set; } = string.Empty;
        public DateOnly DateBorn { get; set; }
        public DateOnly DateHired { get; set; }
        public long CompanyId { get; set; }
    }
}
