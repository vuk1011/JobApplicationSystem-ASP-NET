using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories
{
    public class OfferRepository : Repository<Offer>, IOfferRepository
    {
        public OfferRepository(AppDbContext context) : base(context) { }

        public IEnumerable<Offer> GetByJobApplicationId(long jobApplicationId) =>
            DbSet.Where(o => o.JobApplicationId == jobApplicationId).ToList();

        public Offer? GetByIdWithJobApplication(long id) =>
            DbSet.Include(o => o.JobApplication)
                 .FirstOrDefault(o => o.Id == id);
    }
}
