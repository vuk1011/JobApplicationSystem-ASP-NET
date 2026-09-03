using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories
{
    public class JobApplicationRepository : Repository<JobApplication>, IJobApplicationRepository
    {
        public JobApplicationRepository(AppDbContext context) : base(context) { }

        public bool existsByCandidateIdAndJobPostingId(long candidateId, long jobPosting) =>
            Find(e => e.CandidateId == candidateId && e.JobPostingId == jobPosting).Any();

        public IEnumerable<JobApplication> GetUnmanagedByJobPostingId(long jobPostingId) =>
            DbSet.Include(ja => ja.JobPosting).ThenInclude(jp => jp.Company)
                 .Where(ja => ja.JobPostingId == jobPostingId && ja.EmployeeId == null)
                 .ToList();

        public IEnumerable<JobApplication> GetManagedByEmployeeId(long employeeId) =>
            DbSet.Include(ja => ja.JobPosting).ThenInclude(jp => jp.Company)
                 .Where(ja => ja.EmployeeId == employeeId)
                 .ToList();

        public JobApplication? GetByIdWithDetails(long id) =>
            DbSet.Include(ja => ja.JobPosting).ThenInclude(jp => jp.Company)
                 .Include(ja => ja.Employee)
                 .FirstOrDefault(ja => ja.Id == id);
    }
}
