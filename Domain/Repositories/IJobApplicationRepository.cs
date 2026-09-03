using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Domain.Repositories
{
    public interface IJobApplicationRepository : IRepository<JobApplication>
    {
        bool existsByCandidateIdAndJobPostingId(long candidateId, long jobPosting);
    }
}
