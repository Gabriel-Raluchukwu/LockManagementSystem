using Microsoft.EntityFrameworkCore;

namespace LockManagementSystem.Infrastructure.Persistence;

public class LockManagementReadContext : DbContext
{
    public LockManagementReadContext(DbContextOptions<LockManagementReadContext> options) : base(options)
    {

    }
}