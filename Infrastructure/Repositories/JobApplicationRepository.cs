using Domain.Entities;
using Domain.Repositories;
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
    }
}
