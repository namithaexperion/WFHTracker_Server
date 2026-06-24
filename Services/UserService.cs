using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore;
using WFHTracker_Server.Interface;

namespace WFHTracker_Server.Services;

public class UserService : IUserService
{
    private readonly ApplicationDbContext _context;

    public UserService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<string>> GetUserPrivilegesAsync(string email)
    {
        var user = await _context.Resources
        .FirstOrDefaultAsync(x => x.EmailId == email);

        var userRoles = await _context.UserRoles
            .Where(x => x.ResourceId == user.ResourceId)
            .ToListAsync();


        var accessRoleIds = userRoles
            .Select(x => x.AccessRoleId)
            .Distinct()
            .ToList();

        // Privileges assigned through roles
        var privileges = await (
        from pr in _context.PrivilegeRoles
        join p in _context.Privileges
            on pr.PrivilegeId equals p.PrivilegeId
        where accessRoleIds.Contains(pr.AccessRoleId)
        select p.PrivilegeKey
    )
    .Distinct()
    .ToListAsync();

    return privileges;

    }

}