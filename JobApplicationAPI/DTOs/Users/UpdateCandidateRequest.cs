using Domain.Entities;

namespace JobApplicationAPI.DTOs.Users
{
    public class UpdateCandidateRequest
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public Sex Sex { get; set; }
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
    }
}
