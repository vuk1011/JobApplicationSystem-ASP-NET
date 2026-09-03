using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories
{
    public class InterviewRepository : Repository<Interview>, IInterviewRepository
    {
        public InterviewRepository(AppDbContext context) : base(context) { }

        public IEnumerable<Interview> GetByJobApplicationId(long jobApplicationId) =>
            DbSet.Where(i => i.JobApplicationId == jobApplicationId).ToList();

        public Interview? GetByIdWithJobApplication(long id) =>
            DbSet.Include(i => i.JobApplication)
                 .FirstOrDefault(i => i.Id == id);
    }
}
