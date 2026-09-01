using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations
{
    public class JobPostingEntityTypeConfiguration : IEntityTypeConfiguration<JobPosting>
    {
        public void Configure(EntityTypeBuilder<JobPosting> builder)
        {
            builder
                .Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(3000);

            builder
                .Property(e => e.DatePublished)
                .IsRequired();

            builder
                .Property(e => e.DateExpires)
                .IsRequired();

            builder
                .Ignore(e => e.IsClosed);
        }
    }
}
