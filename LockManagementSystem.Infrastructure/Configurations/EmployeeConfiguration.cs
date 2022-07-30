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

public class EmployeeDetailConfiguration : IEntityTypeConfiguration<EmployeeDetailEntity>
{
    public void Configure(EntityTypeBuilder<EmployeeDetailEntity> builder)
    {
        builder.Property(ed => ed.FirstName)
            .IsRequired();
        
        builder.Property(ed => ed.LastName)
            .IsRequired();
        
        builder.Property(ed => ed.Email)
            .IsRequired();
        
        builder.Property(ed => ed.Gender)
            .IsRequired();
        
        builder.Property(ed => ed.EmploymentDate)
            .IsRequired();
        
        builder.HasIndex(ed => ed.Email)
            .IsUnique();
    }
}