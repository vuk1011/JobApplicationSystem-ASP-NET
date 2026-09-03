using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories
{
    public class JobPostingRepository : Repository<JobPosting>, IJobPostingRepository
    {
        public JobPostingRepository(AppDbContext context) : base(context) { }

        public IEnumerable<JobPosting> GetAllByCompanyId(long companyId) =>
            DbSet.Include(jp => jp.Company)
                 .Where(jp => jp.CompanyId == companyId)
                 .ToList();

        public JobPosting? GetByIdWithCompany(long id) =>
            DbSet.Include(jp => jp.Company)
                 .FirstOrDefault(jp => jp.Id == id);
    }
}
