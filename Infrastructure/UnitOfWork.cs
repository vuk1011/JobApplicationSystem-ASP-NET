using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public void Dispose() => _context.Dispose();
    }
}
