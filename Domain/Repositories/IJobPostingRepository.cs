using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Repositories
{
    public interface IJobPostingRepository : IRepository<JobPosting>
    {
        IEnumerable<JobPosting> GetAllByCompanyId(long companyId);
        JobPosting? GetByIdWithCompany(long id);
    }
}
