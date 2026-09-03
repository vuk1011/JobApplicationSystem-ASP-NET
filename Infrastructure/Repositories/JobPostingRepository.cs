using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class JobPostingRepository : Repository<JobPosting>, IJobPostingRepository
    {
        public JobPostingRepository(AppDbContext context) : base(context) { }

        public IEnumerable<JobPosting> GetAllByCompanyId(long companyId) =>
            DbSet.Include(e => e.Company)
                 .Where(e => e.CompanyId == companyId)
                 .ToList();

        public IEnumerable<JobPosting> GetAllPublished() =>
            DbSet.Include(e => e.Company)
                 .Where(e => e.DateExpires >= DateOnly.FromDateTime(DateTime.Today))
                 .ToList();

        public JobPosting? GetByIdWithCompany(long id) =>
            DbSet.Include(e => e.Company)
                 .FirstOrDefault(e => e.Id == id);
    }
}
