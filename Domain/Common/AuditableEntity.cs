using System.ComponentModel.DataAnnotations;

namespace Domain.Common;

//abstract classes can't be instantiated, but are inherited
public abstract class AuditableEntity
{
    [MaxLength(50)]
    public string? CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }

    [MaxLength(50)]
    public string? LastModifiedBy { get; set; }
    public DateTime? LastModifiedDate { get; set; }
}
