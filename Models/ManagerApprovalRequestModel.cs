public class ManagerApprovalRequestModel
{
    public string MailId { get; set; } = string.Empty;

    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }

    public string FrequencyType { get; set; } = string.Empty;

    public int FrequencyDays { get; set; }

    public string ReasonCategory { get; set; } = string.Empty;

    public string? ManagerComments { get; set; }
}