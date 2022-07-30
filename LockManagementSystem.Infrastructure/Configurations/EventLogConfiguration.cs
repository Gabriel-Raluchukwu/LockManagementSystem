using LockManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LockManagementSystem.Infrastructure.Configurations;

public class EventLogConfiguration : IEntityTypeConfiguration<EventLogEntity>
{
    public void Configure(EntityTypeBuilder<EventLogEntity> builder)
    {
        builder.HasIndex(e => e.CreatedAt);
    }
}