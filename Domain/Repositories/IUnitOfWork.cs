using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        ICandidateRepository Candidates { get; }
        IEmployeeRepository Employees { get; }
        ICompanyRepository Companies { get; }
        IJobPostingRepository JobPostings { get; }
        IOfferRepository Offers { get; }
        IJobApplicationRepository JobApplications { get; }
        IInterviewRepository Interviews { get; }

        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}
