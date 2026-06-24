using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WFHTracker.Entities;
public class Privileges : BaseEntity
{
    [Key]
    public long PrivilegeId { get; set; }
    [Required]
    [StringLength(50)]
    public string PrivilegeName { get; set;} = string.Empty;
    [Required]
    [StringLength(50)]
    public string PrivilegeKey { get; set; } = string.Empty;
}