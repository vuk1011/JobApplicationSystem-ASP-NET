using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Repositories
{
    public interface IInterviewRepository : IRepository<Interview>
    {
        IEnumerable<Interview> GetByJobApplicationId(long jobApplicationId);
        Interview? GetByIdWithJobApplication(long id);
    }
}
