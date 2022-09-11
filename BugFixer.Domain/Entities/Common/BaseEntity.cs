namespace BugFixer.Domain.Entities.Common;

public class BaseEntity
{
    public long Id { get; set; }
    public DateTime CreationDate { get; set; } = DateTime.Now;
    public bool IsDeleted { get; set; } = false;
}