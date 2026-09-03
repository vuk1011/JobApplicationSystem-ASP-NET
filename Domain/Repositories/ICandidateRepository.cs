using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Repositories
{
    public interface ICandidateRepository : IRepository<Candidate>
    {
        Task<Candidate?> GetByAppUserIdAsync(string appUserId);
    }
}
