using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Repositories
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task<Employee?> GetByAppUserIdAsync(string appUserId);
    }
}
