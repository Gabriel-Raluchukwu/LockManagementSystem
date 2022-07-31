using LockManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LockManagementSystem.Infrastructure.Configurations;

public class EmployeeRoleConfiguration : IEntityTypeConfiguration<EmployeeRoleEntity>
{
    public void Configure(EntityTypeBuilder<EmployeeRoleEntity> builder)
    {
        builder.HasIndex(ur => new {ur.EmployeeId, ur.RoleId});

        builder.HasOne(er => er.Employee)
            .WithMany(e => e.EmployeeRoles)
            .HasForeignKey(er => er.EmployeeId);

        builder.HasOne(er => er.Role)
            .WithMany(r => r.EmployeeRoles)
            .HasForeignKey(er => er.RoleId);
    }
}