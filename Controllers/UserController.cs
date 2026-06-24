using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using WFHTracker_Server.Interface;

namespace WFHTracker_Server.Controllers;

[ApiController]
[Route("api/user")]
[Authorize]
public class UserController : ControllerBase
{
    private readonly ILogger<EmployeeController> _logger;
    private readonly ApplicationDbContext _context;

    private readonly IUserService _userService;

    public UserController(
        ILogger<EmployeeController> logger,
        ApplicationDbContext context,
        IUserService userService)
    {
        _logger = logger;
        _context = context;
        _userService = userService;
    }



[HttpGet("privileges")]
public async Task<IActionResult> GetPrivileges()
{
    var email = User.Identity?.Name;

    if (string.IsNullOrEmpty(email))
        return Unauthorized("Email not found in token");

    var user = await _context.Resources
        .FirstOrDefaultAsync(x => x.EmailId == email);

    if (user == null)
         return Ok(new
        {
            Email = email,
            RoleId = "",
            Privileges = ""
        });

    //  // Roles assigned to user
    //     var userRoles = await _context.UserRoles
    //         .Where(x => x.ResourceId == user.ResourceId)
    //         .ToListAsync();

    //     if (!userRoles.Any())
    //         return Forbid();

    //     var accessRoleIds = userRoles
    //         .Select(x => x.AccessRoleId)
    //         .Distinct()
    //         .ToList();

    //     // Privileges assigned through roles
    //     var privileges = await (
    //     from pr in _context.PrivilegeRoles
    //     join p in _context.Privileges
    //         on pr.PrivilegeId equals p.PrivilegeId
    //     where accessRoleIds.Contains(pr.AccessRoleId)
    //     select p.PrivilegeKey
    // )
    // .Distinct()
    // .ToListAsync();

    var privileges = _userService.GetUserPrivilegesAsync(email);

    return Ok(new
    {
        Privileges = privileges
    });
}
}