namespace PalpiteFC.Api.Application.Responses;

public class WaitingListResponse
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Team { get; set; }
    public DateOnly? Birthday { get; set; }
    public int Age { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
}