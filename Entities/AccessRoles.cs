using System.ComponentModel.DataAnnotations;
namespace WFHTracker.Entities;
public class AccessRoles:BaseEntity
{
    [Key]
    public long AccessRoleId { get; set; }
    [Required]
    [StringLength(50)]

    public string RoleName { get; set; } = string.Empty;
}