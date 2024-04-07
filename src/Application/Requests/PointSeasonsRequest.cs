namespace PalpiteFC.Api.Application.Requests;

public class PointSeasonsRequest
{
    public int Id { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}