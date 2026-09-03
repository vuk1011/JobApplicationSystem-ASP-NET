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

        IEnumerable<JobApplication> GetUnmanagedByJobPostingId(long jobPostingId);
        IEnumerable<JobApplication> GetManagedByEmployeeId(long employeeId);
        JobApplication? GetByIdWithDetails(long id);

        IEnumerable<JobApplication> GetByCandidateId(long candidateId);
        JobApplication? GetByIdForCandidate(long id, long candidateId);
    }
}
