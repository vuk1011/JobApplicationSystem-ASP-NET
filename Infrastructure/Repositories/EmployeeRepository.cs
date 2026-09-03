using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(AppDbContext context) : base(context) { }

        public Task<Employee?> GetByAppUserIdAsync(string appUserId) =>
            DbSet.FirstOrDefaultAsync(e => e.AppUserId == appUserId);
    }
}
