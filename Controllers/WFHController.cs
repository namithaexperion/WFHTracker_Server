using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WFHTracker.Entities;
using Microsoft.AspNetCore.Authorization;

namespace WFHTracker_Server.Controllers;

[ApiController]
[Route("api/wfh")]
[Authorize]
public class WFHController : ControllerBase
{
    private readonly ILogger<WFHController> _logger;
    private readonly ApplicationDbContext _context;

    private readonly EmailService _emailService;

    public WFHController(
        ILogger<WFHController> logger,
        ApplicationDbContext context,
        EmailService emailService)
    {
        _logger = logger;
        _context = context;
         _emailService = emailService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateWFHRequest(
        [FromBody] WFHRequestModel request)
    {
        if (request.FromDate > request.ToDate)
        {
            return BadRequest("FromDate cannot be greater than ToDate.");
        }

        var wfh = new WFHRequests
        {
            FromDate = request.FromDate,
            ToDate = request.ToDate,
            Reason = request.Reason,
            ReasonCategory = request.ReasonCategory,

            // Default values
            Status = "Pending-Manager-approval",

            // Populate these from logged-in user later
            EmployeeMailId = "namitha.augustine@gmail.com",
            ManagerMailId = "namitha.augustine@gmail.com",
            HRMailId = "namitha.augustine@gmail.com",
            GMMailId = "namitha.augustine@gmail.com"
        };

        _context.WFHRequests.Add(wfh);
        await _context.SaveChangesAsync();

        await _emailService.SendManagerApprovalEmail();

        return Ok(new
        {
            Message = "WFH request submitted successfully"
        });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetWFHRequest(int id)
    {
        var request = await _context.WFHRequests.FindAsync(id);

        if (request == null)
        {
            return NotFound();
        }

        return Ok(request);
    }

    [HttpPut("reject/{id}")]
    public async Task<IActionResult> RejectWFHRequest(int id)
    {
    // var request = await _context.WFHRequests.FindAsync(id);

    // if (request == null)
    // {
    //     return NotFound($"WFH request with Id {id} not found.");
    // }

    // request.Status = "Rejected";

    // await _context.SaveChangesAsync();

    await _emailService.SendHRApprovalEmail();

    return Ok(new
    {
        Message = "WFH request rejected successfully."
    });
}

[HttpGet("{mailid}")]
[HttpGet("approvals/{email}")]
public async Task<IActionResult> GetPendingApprovals(string email)
{
    if (string.IsNullOrWhiteSpace(email))
    {
        return BadRequest("Email is required.");
    }

    var requests = await _context.WFHRequests
        .Where(x =>
            (x.ManagerMailId == email &&
             x.Status == "Pending-Manager-Approval")
            ||
            (x.GMMailId == email &&
             x.Status == "Pending-GM-Approval")
            ||
            (x.HRMailId == email &&
             x.Status == "Pending-HR-Approval")
        )
        .OrderByDescending(x => x.EmployeeMailId)
        .ToListAsync();

    if (!requests.Any())
    {
        return NotFound($"No pending approvals found for '{email}'.");
    }

    return Ok(requests);
}

[HttpPut("{id}/manager-approve")]
public async Task<IActionResult> ManagerApproveWFHRequest(
    int id,
    [FromBody] ManagerApprovalRequestModel request)
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

    // Validate manager and status
    if (!string.Equals(
            wfh.ManagerMailId,
            request.MailId,
            StringComparison.OrdinalIgnoreCase))
    {
        return Forbid("You are not authorized to approve this request.");
    }

    if (wfh.Status != "Pending-Manager-Approval")
    {
        return BadRequest(
            $"Request cannot be processed because current status is '{wfh.Status}'.");
    }

    // Update allowed fields
    wfh.FromDate = request.FromDate;
    wfh.ToDate = request.ToDate;
    wfh.FrequencyType = request.FrequencyType;
    wfh.FrequencyDays = request.FrequencyDays;
    wfh.ReasonCategory = request.ReasonCategory;
    wfh.ManagerComment = request.ManagerComments;

    // Calculate number of days
    var totalDays = (wfh.ToDate.Date - wfh.FromDate.Date).Days + 1;

    // Update status based on duration
    if (totalDays > 15)
    {
        wfh.Status = "Pending-GM-Approval";
    }
    else
    {
        wfh.Status = "Pending-HR-Approval";
    }

    await _context.SaveChangesAsync();

    return Ok(new
    {
        Message = "WFH request approved by Manager successfully.",
        RequestId = wfh.EmployeeMailId,
        NewStatus = wfh.Status
    });
}
}