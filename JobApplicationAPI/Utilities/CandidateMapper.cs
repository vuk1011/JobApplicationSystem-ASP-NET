using Domain.Entities;
using Infrastructure.Identity;
using JobApplicationAPI.DTOs.Users;

namespace JobApplicationAPI.Utilities
{
    public static class CandidateMapper
    {
        public static CandidateDto ToDto(Candidate candidate, AppUser appUser) => new()
        {
            Id = candidate.Id,
            FirstName = candidate.FirstName,
            LastName = candidate.LastName,
            Sex = candidate.Sex,
            Address = candidate.Address,
            Email = appUser.Email ?? string.Empty,
            Phone = appUser.PhoneNumber ?? string.Empty,
        };
    }
}
