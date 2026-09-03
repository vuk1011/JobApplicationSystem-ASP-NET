using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CandidateRepository : Repository<Candidate>, ICandidateRepository
    {
        public CandidateRepository(AppDbContext context) : base(context) { }

        public Task<Candidate?> GetByAppUserIdAsync(string appUserId) =>
            DbSet.FirstOrDefaultAsync(e => e.AppUserId == appUserId);
    }
}
