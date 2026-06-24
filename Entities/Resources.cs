using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WFHTracker.Entities;

public class Resources : BaseEntity
{
    [Key]
    public long ResourceId { get; set; }
    [Required]
    [StringLength(50)]

     public string EmailId { get; set; } = string.Empty;
}