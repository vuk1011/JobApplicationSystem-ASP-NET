using Domain.Entities;
using Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations
{
    public class EmployeeEntityTypeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder
                .HasOne<AppUser>()
                .WithOne()
                .HasForeignKey<Employee>(e => e.AppUserId)
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
                .Property(e => e.NationalId)
                .IsRequired()
                .HasMaxLength(20);

            builder
                .Property(e => e.DateBorn)
                .IsRequired();

            builder
                .Property(e => e.DateHired)
                .IsRequired();

            builder
                .HasMany(e => e.ManagedJobApplications)
                .WithOne(e => e.Employee)
                .HasForeignKey(e => e.EmployeeId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
