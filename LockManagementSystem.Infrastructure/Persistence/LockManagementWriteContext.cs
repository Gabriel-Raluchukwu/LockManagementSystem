using System.Reflection;
using LockManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LockManagementSystem.Infrastructure.Persistence;

public class LockManagementWriteContext : DbContext
{
    public LockManagementWriteContext(DbContextOptions<LockManagementWriteContext> options) : base(options)
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