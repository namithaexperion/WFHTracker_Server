namespace WFHTracker_Server;

public class WFH
{
    public int Id { get; set; }

    public string EmployeeMailId { get; set; } = string.Empty;

    public string ManagerMailId { get; set; } = string.Empty;

    public string GMMailId { get; set; } = string.Empty;

    public string HRMailId { get; set; } = string.Empty;

    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }

    public string Reason { get; set; } = string.Empty;

    public string ReasonCategory { get; set; } = string.Empty;

    public int? FrequencyDays { get; set; }

    public string? FrequencyType { get; set; } = string.Empty;

    public string? ManagerComment { get; set; }

    public string? BHRComment { get; set; }

    public string? GMComment { get; set; }

    public string? Status { get; set; } = string.Empty;
}
