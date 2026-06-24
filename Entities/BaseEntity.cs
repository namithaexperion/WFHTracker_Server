namespace WFHTracker.Entities;

public class BaseEntity
{
    public DateTime CreatedDate { get; set; } 
    public long CreatedBy { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public long? UpdatedBy { get; set; }
}
