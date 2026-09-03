using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class OfferRepository : Repository<Offer>, IOfferRepository
    {
        public OfferRepository(AppDbContext context) : base(context) { }

        public IEnumerable<Offer> GetByJobApplicationId(long jobApplicationId) =>
            DbSet.Where(e => e.JobApplicationId == jobApplicationId).ToList();

        public Offer? GetByIdWithJobApplication(long id) =>
            DbSet.Include(e => e.JobApplication)
                 .FirstOrDefault(e => e.Id == id);
    }
}
