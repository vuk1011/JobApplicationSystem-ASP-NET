using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class JobApplicationRepository : Repository<JobApplication>, IJobApplicationRepository
    {
        public JobApplicationRepository(AppDbContext context) : base(context) { }

        public bool existsByCandidateIdAndJobPostingId(long candidateId, long jobPosting) =>
            Find(e => e.CandidateId == candidateId && e.JobPostingId == jobPosting).Any();

        public IEnumerable<JobApplication> GetUnmanagedByJobPostingId(long jobPostingId) =>
            DbSet.Include(e => e.JobPosting).ThenInclude(e => e.Company)
                 .Where(e => e.JobPostingId == jobPostingId && e.EmployeeId == null)
                 .ToList();

        public IEnumerable<JobApplication> GetManagedByEmployeeId(long employeeId) =>
            DbSet.Include(e => e.JobPosting).ThenInclude(e => e.Company)
                 .Where(e => e.EmployeeId == employeeId)
                 .ToList();

        public JobApplication? GetByIdWithDetails(long id) =>
            DbSet.Include(e => e.JobPosting).ThenInclude(e => e.Company)
                 .Include(e => e.Employee)
                 .FirstOrDefault(e => e.Id == id);

        public IEnumerable<JobApplication> GetByCandidateId(long candidateId) =>
            DbSet.Include(e => e.JobPosting).ThenInclude(e => e.Company)
                 .Where(e => e.CandidateId == candidateId)
                 .ToList();

        public JobApplication? GetByIdForCandidate(long id, long candidateId) =>
            DbSet.Include(e => e.JobPosting).ThenInclude(e => e.Company)
                 .FirstOrDefault(e => e.Id == id && e.CandidateId == candidateId);
    }
}
