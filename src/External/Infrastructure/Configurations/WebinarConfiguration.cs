using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public sealed class WebinarConfiguration : IEntityTypeConfiguration<Webinar>
{
    public void Configure(EntityTypeBuilder<Webinar> builder)
    {
        builder.ToTable("Webinars");
        builder.HasKey(w => w.Id);
        builder.Property(w => w.Name).HasMaxLength(100);
        builder.Property(w => w.ScheduleOn).IsRequired();
    }
}
