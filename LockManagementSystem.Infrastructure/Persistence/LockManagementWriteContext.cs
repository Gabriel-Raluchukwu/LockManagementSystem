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
    public DbSet<LockRoleEntity> LockRoles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        
        SeedDatabase(modelBuilder);
    }

    private void SeedDatabase(ModelBuilder modelBuilder)
    {
        var officeId = Guid.NewGuid();
        var employeeDetailEntityId = Guid.NewGuid();
        modelBuilder.Entity<OfficeEntity>()
            .HasData(new OfficeEntity
            {
                Id = officeId,
                Address = "No 20, Johnson avenue",
                Country = "Nigeria",
                Name = "Clay Locks",
                Description = "Locks and Security",
                NumberOfDoors = 24,
                NumberOfLocks = 12,
            });

        modelBuilder.Entity<EmployeeDetailEntity>()
            .HasData(new EmployeeDetailEntity
            {
                Id = employeeDetailEntityId,
                Address = "No 12, Palace road",
                Country = "Nigeria",
                Email = "default.user@clay.com",
                Gender = "Male",
                Nationality = "Nigerian",
                State = "Lagos",
                EmploymentDate = DateTime.UtcNow,
                FirstName = "default",
                LastName = "user",
                PhoneNumber = "23411111111",
                OfficeId = officeId,
                DateOfBirth = DateTime.MinValue
            });

        var employeeId = employeeDetailEntityId;
        
        modelBuilder.Entity<EmployeeEntity>()
            .HasData(new EmployeeEntity
            {
                Id = employeeId,
                Email = "default.user@clay.com",
                PasswordHash = "#CLAY#2000$rFAUch4AxrxF+3gvCxY3IjdZHpwNUMNUh84rPQ39QOmNEPyl", // Password hash for 'Login@1234'
            });

        var entranceLockId = Guid.NewGuid();
        var storageLockId = Guid.NewGuid();
        
        modelBuilder.Entity<LockEntity>()
            .HasData(
                new LockEntity
                {
                    Id = entranceLockId,
                    Location = "Main Entrance, Ground floor",
                    OfficeId = officeId,
                    SerialNo = "123454fd",
                    DateInstalled = DateTime.UtcNow,
                    Model = "Clay Lock 2.0"
                },
                new LockEntity
                {
                    Id = storageLockId,
                    Location = "Storage Entrance, Ground floor",
                    OfficeId = officeId,
                    SerialNo = "40478872",
                    DateInstalled = DateTime.UtcNow,
                    Model = "Clay Lock 2.0"
                });

        var roleAdminId = Guid.NewGuid();
        var roleEmployeeId = Guid.NewGuid();
        var roleDirectorId = Guid.NewGuid();
        var roleManagerId = Guid.NewGuid();
        
        modelBuilder.Entity<RoleEntity>()
            .HasData(
                new RoleEntity
                {
                    Id = roleAdminId,
                    OfficeId = officeId,
                    Name = "Administrator",
                    Description = "Administrator role"
                },
                new RoleEntity
                {
                    Id = roleEmployeeId,
                    OfficeId = officeId,
                    Name = "Employee",
                    Description = "Employee role"
                },
                new RoleEntity
                {
                    Id = roleDirectorId,
                    OfficeId = officeId,
                    Name = "Director",
                    Description = "Director role"
                },
                new RoleEntity
                {
                    Id = roleManagerId,
                    OfficeId = officeId,
                    Name = "OfficeManager",
                    Description = "Director role"
                });

        modelBuilder.Entity<EmployeeRoleEntity>()
            .HasData(
                new EmployeeRoleEntity
                {
                    Id = Guid.NewGuid(),
                    EmployeeId = employeeId,
                    RoleId = roleAdminId,
                },
                new EmployeeRoleEntity
                {
                    Id = Guid.NewGuid(),
                    EmployeeId = employeeId,
                    RoleId = roleEmployeeId,
                },
                new EmployeeRoleEntity
                {
                    Id = Guid.NewGuid(),
                    EmployeeId = employeeId,
                    RoleId = roleDirectorId,
                },
                new EmployeeRoleEntity
                {
                    Id = Guid.NewGuid(),
                    EmployeeId = employeeId,
                    RoleId = roleManagerId,
                });

        modelBuilder.Entity<LockRoleEntity>()
            .HasData(
                new LockRoleEntity
                {
                    Id = Guid.NewGuid(),
                    LockId = entranceLockId,
                    RoleId = roleEmployeeId
                },
                new LockRoleEntity
                {
                    Id = Guid.NewGuid(),
                    LockId = storageLockId,
                    RoleId = roleAdminId
                },
                new LockRoleEntity
                {
                    Id = Guid.NewGuid(),
                    LockId = storageLockId,
                    RoleId = roleManagerId
                },
                new LockRoleEntity
                {
                    Id = Guid.NewGuid(),
                    LockId = storageLockId,
                    RoleId = roleDirectorId
                });
    }
}