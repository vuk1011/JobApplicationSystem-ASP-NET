using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class InterviewRepository : Repository<Interview>, IInterviewRepository
    {
        public InterviewRepository(AppDbContext context) : base(context) { }

        public IEnumerable<Interview> GetByJobApplicationId(long jobApplicationId) =>
            DbSet.Where(e => e.JobApplicationId == jobApplicationId).ToList();

        public Interview? GetByIdWithJobApplication(long id) =>
            DbSet.Include(e => e.JobApplication)
                 .FirstOrDefault(e => e.Id == id);
    }
}
