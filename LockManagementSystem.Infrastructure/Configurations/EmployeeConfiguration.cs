using LockManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LockManagementSystem.Infrastructure.Configurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<EmployeeEntity>
{
    public void Configure(EntityTypeBuilder<EmployeeEntity> builder)
    {
        builder.HasOne(e => e.Detail)
            .WithOne(ed => ed.Employee)
            .HasForeignKey<EmployeeDetailEntity>(e => e.EmployeeId);
    }
}