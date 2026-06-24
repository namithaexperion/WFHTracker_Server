
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WFHTracker.Entities;
public class UserRoles : BaseEntity
{
    [Key]
    public long UserRoleId { get; set; }
    [Required]
    public Resources Resource { get; set; }
    [ForeignKey(nameof(Resource))]
    public long ResourceId { get; set; }
    [Required]
    public AccessRoles Role { get; set; }
    [ForeignKey(nameof(Role))]
    public long AccessRoleId { get; set; }
}