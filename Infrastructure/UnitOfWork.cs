using Domain.Repositories;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        private ICandidateRepository? _candidates;
        private IEmployeeRepository? _employees;
        private ICompanyRepository? _companies;
        private IJobPostingRepository? _jobPostings;
        private IOfferRepository? _offers;
        private IJobApplicationRepository? _jobApplications;
        private IInterviewRepository? _interviews;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public ICandidateRepository Candidates =>
            _candidates ??= new CandidateRepository(_context);

        public IEmployeeRepository Employees =>
            _employees ??= new EmployeeRepository(_context);

        public ICompanyRepository Companies =>
            _companies ??= new CompanyRepository(_context);

        public IJobPostingRepository JobPostings =>
            _jobPostings ??= new JobPostingRepository(_context);

        public IOfferRepository Offers =>
            _offers ??= new OfferRepository(_context);

        public IJobApplicationRepository JobApplications =>
            _jobApplications ??= new JobApplicationRepository(_context);

        public IInterviewRepository Interviews =>
            _interviews ??= new InterviewRepository(_context);

        public Task<int> SaveChangesAsync() => _context.SaveChangesAsync();

        public void Dispose() => _context.Dispose();
    }
}
