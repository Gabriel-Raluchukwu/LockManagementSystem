using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LockManagementSystem.Domain.Entities;

public class Entity : BaseEntity
{
    protected Entity()
    {
        UpdatedAt = DateTime.UtcNow;
    }
    
    public Guid UpdatedBy { get; set; }
    
    public DateTime UpdatedAt { get; set; }
    
}

public class BaseEntity
{
    protected BaseEntity()
    {
        CreatedAt = DateTime.UtcNow;    
    }
    
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    public Guid CreatedBy { get; set; }
    
    public DateTime CreatedAt { get; set; } 

    public bool IsDeprecated { get; set; }

    public DateTime DeprecatedAt { get; set; }
}