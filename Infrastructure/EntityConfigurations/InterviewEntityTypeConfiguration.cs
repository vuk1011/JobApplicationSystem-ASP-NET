using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations
{
    public class InterviewEntityTypeConfiguration : IEntityTypeConfiguration<Interview>
    {
        public void Configure(EntityTypeBuilder<Interview> builder)
        {
            builder
                .Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(200);

            builder
                .Property(e => e.TimeScheduled)
                .IsRequired();
        }
    }
}
