using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public class AppUser : IdentityUser
    {
        public UserType UserType { get; set; }
    }

    public enum UserType
    {
        Candidate,
        Employee,
    }
}
