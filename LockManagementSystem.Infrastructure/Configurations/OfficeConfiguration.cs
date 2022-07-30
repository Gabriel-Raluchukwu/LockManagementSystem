using LockManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LockManagementSystem.Infrastructure.Configurations;

public class OfficeConfiguration : IEntityTypeConfiguration<OfficeEntity>
{
    public void Configure(EntityTypeBuilder<OfficeEntity> builder)
    {
        builder.Property(o => o.Address)
            .IsRequired();
    }
}