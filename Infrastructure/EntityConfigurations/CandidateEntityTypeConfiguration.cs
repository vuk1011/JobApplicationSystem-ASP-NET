using Domain.Entities;
using Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations
{
    public class CandidateEntityTypeConfiguration : IEntityTypeConfiguration<Candidate>
    {
        public void Configure(EntityTypeBuilder<Candidate> builder)
        {
            builder
                .HasOne<AppUser>()
                .WithOne()
                .HasForeignKey<Candidate>(e => e.AppUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(30);

            builder
                .Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(30);

            builder
                .Property(e => e.Sex)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(10);

            builder
                .Property(e => e.Address)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .HasMany(e => e.JobApplications)
                .WithOne(e => e.Candidate)
                .HasForeignKey(e => e.CandidateId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
