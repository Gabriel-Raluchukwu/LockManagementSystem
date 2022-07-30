using LockManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LockManagementSystem.Infrastructure.Configurations;

public class LockConfiguration : IEntityTypeConfiguration<LockEntity>
{
    public void Configure(EntityTypeBuilder<LockEntity> builder)
    {
        builder.HasOne(l => l.Office)
            .WithMany(o => o.Locks)
            .HasForeignKey(lk => lk.OfficeId);
    }
}