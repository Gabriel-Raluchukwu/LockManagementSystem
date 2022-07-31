using System.Reflection;
using LockManagementSystem.Domain.Entities;

namespace LockManagementSystem.Infrastructure.Persistence;

public class LockManagementWriteContext : DbContext
{
    public LockManagementWriteContext(DbContextOptions<LockManagementWriteContext> options) : base(options)
    {

    }
    
    public DbSet<EmployeeEntity> Employees { get; set; }
    public DbSet<EmployeeDetailEntity> EmployeeDetails { get; set; }
    public DbSet<OfficeEntity> Offices { get; set; }
    public DbSet<LockEntity> Locks { get; set; }
    public DbSet<EventLogEntity> EventLogs { get; set; }
    public DbSet<RoleEntity> Roles { get; set; }
    public DbSet<EmployeeRoleEntity> EmployeeRoles { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        
        SeedDatabase(modelBuilder);
    }

    private void SeedDatabase(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RoleEntity>()
            .HasData(
                new RoleEntity
                {
                    Name = "Administrator",
                    Description = "Administrator role"
                },
                new RoleEntity
                {
                    Name = "Employee",
                    Description = "Employee role"
                },
                new RoleEntity
                {
                    Name = "Director",
                    Description = "Director role"
                },
                new RoleEntity
                {
                    Name = "OfficeManager",
                    Description = "Director role"
                });
    }
}