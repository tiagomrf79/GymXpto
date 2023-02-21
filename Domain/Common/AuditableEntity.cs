namespace Domain.Common;

//abstract classes can't be instantiated, but are inherited
public abstract class AuditableEntity
{
    public string? CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public string? LastModifiedBy { get; set; }
    public DateTime? LastModifiedDate { get; set; }
}
