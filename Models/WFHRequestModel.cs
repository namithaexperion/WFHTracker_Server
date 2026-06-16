public class WFHRequestModel
{
    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }

    public string Reason { get; set; } = string.Empty;

    public string ReasonCategory { get; set; } = string.Empty;
}