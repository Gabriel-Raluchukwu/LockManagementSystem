using System.Reflection;
using LockManagementSystem.Domain.Entities;

namespace LockManagementSystem.Infrastructure.Persistence;

public class LockManagementReadContext : DbContext
{
    public LockManagementReadContext(DbContextOptions<LockManagementReadContext> options) : base(options)
    {

    }

    public DbSet<EmployeeEntity> Employees { get; set; }
    public DbSet<EmployeeDetailEntity> EmployeeDetails { get; set; }
    public DbSet<OfficeEntity> Offices { get; set; }
    public DbSet<LockEntity> Locks { get; set; }
    public DbSet<EventLogEntity> EventLogs { get; set; }
    public DbSet<RoleEntity> Roles { get; set; }
    public DbSet<EmployeeRoleEntity> EmployeeRoles { get; set; }
    public DbSet<LockRoleEntity> LockRoles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}