using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public sealed class WebinarConfiguration : IEntityTypeConfiguration<Webinar>
{
    public void Configure(EntityTypeBuilder<Webinar> builder)
    {
        _ = builder.ToTable("Webinars");
        _ = builder.HasKey(w => w.Id);
        _ = builder.Property(w => w.Name).HasMaxLength(100);
        _ = builder.Property(w => w.ScheduleOn).IsRequired();
    }
}
