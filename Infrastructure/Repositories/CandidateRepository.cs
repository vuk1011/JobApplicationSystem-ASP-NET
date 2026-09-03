using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories
{
    public class CandidateRepository : Repository<Candidate>, ICandidateRepository
    {
        public CandidateRepository(AppDbContext context) : base(context) { }

        public Task<Candidate?> GetByAppUserIdAsync(string appUserId) =>
            DbSet.FirstOrDefaultAsync(c => c.AppUserId == appUserId);
    }
}
