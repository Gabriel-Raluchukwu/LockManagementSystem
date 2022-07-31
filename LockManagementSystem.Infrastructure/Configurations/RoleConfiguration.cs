using LockManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LockManagementSystem.Infrastructure.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<RoleEntity>
{
    public void Configure(EntityTypeBuilder<RoleEntity> builder)
    {
        builder.Property(r => r.Name)
            .IsRequired();
        
        builder.HasIndex(r => r.Name)
            .IsUnique();

        builder.HasOne(r => r.Office)
            .WithMany(o => o.Roles)
            .HasForeignKey(r => r.OfficeId);
    }
}