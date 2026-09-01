using Domain.Entities;
using Infrastructure.EntityConfigurations;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<JobPosting> JobPostings { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<JobApplication> JobApplications { get; set; }
        public DbSet<Interview> Interviews { get; set; }
        public DbSet<Company> Companies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityUserPasskey<string>>(passkey =>
            {
                passkey.HasKey(p => new { p.UserId, p.CredentialId });
                passkey.ComplexProperty(p => p.Data);
            });

            new CandidateEntityTypeConfiguration().Configure(modelBuilder.Entity<Candidate>());
            new EmployeeEntityTypeConfiguration().Configure(modelBuilder.Entity<Employee>());
            new JobPostingEntityTypeConfiguration().Configure(modelBuilder.Entity<JobPosting>());
            new OfferEntityTypeConfiguration().Configure(modelBuilder.Entity<Offer>());
            new JobApplicationEntityTypeConfiguration().Configure(modelBuilder.Entity<JobApplication>());
            new InterviewEntityTypeConfiguration().Configure(modelBuilder.Entity<Interview>());
            new CompanyEntityTypeConfiguration().Configure(modelBuilder.Entity<Company>());
        }
    }
}