using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LockManagementSystem.Domain.Entities;

public class Entity : BaseEntity
{
    public Guid CreatedBy { get; set; }
    
    public DateTime CreatedAt { get; set; } 
}

public class BaseEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }   
    
    public Guid UpdatedBy { get; set; }
    
    public DateTime UpdatedAt { get; set; }

    public bool IsDeprecated { get; set; }

    public DateTime DeprecatedAt { get; set; }
}