
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WFHTracker.Entities;
public class PrivilegeRoles : BaseEntity
{
    [Key]
    public long PrivilegeRoleId { get; set; }
     [Required]
    [StringLength(50)]
    public AccessRoles Role { get; set; }
    [ForeignKey(nameof(Role))]
    public long AccessRoleId { get; set; }
    public Privileges Privilege { get; set; }
    [ForeignKey(nameof(Privilege))]
    public long PrivilegeId { get; set; }

}
