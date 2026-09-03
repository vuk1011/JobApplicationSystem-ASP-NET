using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JobApplicationAPI.Services
{
    public class JwtService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;

        public JwtService(UserManager<AppUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<string> CreateTokenAsync(AppUser user, string fullName)
        {
            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim("FullName", fullName),
            };
            claims.AddRange(roles
                .Select(r => new Claim(ClaimTypes.Role, r)));

            var jwtSection = _configuration.GetSection("Jwt");
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSection["Key"]!));
            var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            var expiryMinutes = double.Parse(jwtSection["ExpiryMinutes"]!);

            var token = new JwtSecurityToken(
                 issuer: jwtSection["Issuer"],
                 audience: jwtSection["Audience"],
                 claims: claims,
                 expires: DateTime.UtcNow.AddMinutes(expiryMinutes),
                 signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
