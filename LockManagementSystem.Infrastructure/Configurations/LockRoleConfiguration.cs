using LockManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LockManagementSystem.Infrastructure.Configurations;

public class LockRoleConfiguration : IEntityTypeConfiguration<LockRoleEntity>
{
    public void Configure(EntityTypeBuilder<LockRoleEntity> builder)
    {
        builder.HasOne(lk => lk.Lock)
            .WithMany(lre => lre.LockRoles)
            .HasForeignKey(lk => lk.LockId);

        builder.HasOne(lk => lk.Role)
            .WithMany(lre => lre.LockRoles)
            .HasForeignKey(lk => lk.RoleId);
    }
}