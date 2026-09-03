using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace JobApplicationAPI.Services
{
    public class JwtService
    {
        private readonly UserManager<AppUser> userManager;

        public JwtService(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }
    }
}
