using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WFHTracker_Server.Controllers;

[ApiController]
[Route("[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly ILogger<EmployeeController> _logger;
    private readonly ApplicationDbContext _context;

    private readonly EmailService _emailService;

    public EmployeeController(
        ILogger<EmployeeController> logger,
        ApplicationDbContext context,
        EmailService emailService)
    {
        _logger = logger;
        _context = context;
         _emailService = emailService;
    }


[HttpGet("{employeeMailId}")]
public async Task<IActionResult> GetWFHRequestsByEmployee(string employeeMailId)
{
    if (employeeMailId == null)
    {
        return BadRequest("EmployeeId is required.");
    }

    var requests = await _context.WFHRequests
        .Where(x => x.EmployeeMailId == employeeMailId)
        .OrderByDescending(x => x.Id)
        .ToListAsync();

    if (!requests.Any())
    {
        return NotFound($"No WFH requests found for EmployeeId '{employeeMailId}'.");
    }

    return Ok(requests);
}

[HttpPut("{id}")]
public async Task<IActionResult> UpdateWFHRequest(
    int id,
    [FromBody] WFHRequestModel request)
{
    if (request.FromDate > request.ToDate)
    {
        return BadRequest("FromDate cannot be greater than ToDate.");
    }

    var wfh = await _context.WFHRequests.FindAsync(id);

    if (wfh == null)
    {
        return NotFound($"WFH request with Id {id} not found.");
    }

    if (wfh.Status != "Pending-Manager-approval")
    {
        return BadRequest(
            $"WFH request cannot be edited because its current status is '{wfh.Status}'.");
    }

    wfh.FromDate = request.FromDate;
    wfh.ToDate = request.ToDate;
    wfh.Reason = request.Reason;
    wfh.ReasonCategory = request.ReasonCategory;

    await _context.SaveChangesAsync();

    return Ok(new
    {
        Message = "WFH request updated successfully.",
        WFHRequest = wfh
    });
}
}