using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations
{
    public class JobApplicationEntityTypeConfiguration : IEntityTypeConfiguration<JobApplication>
    {
        public void Configure(EntityTypeBuilder<JobApplication> builder)
        {
            builder
                .Property(e => e.DateSubmitted)
                .IsRequired();

            builder
                .Property(e => e.Status)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(20);

            builder
                .HasMany(e => e.Offers)
                .WithOne(e => e.JobApplication)
                .HasForeignKey(e => e.JobApplicationId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasMany(e => e.Interviews)
                .WithOne(e => e.JobApplication)
                .HasForeignKey(e => e.JobApplicationId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Ignore(e => e.IsManaged);
        }
    }
}
