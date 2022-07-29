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
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}