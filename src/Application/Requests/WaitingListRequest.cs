using PalpiteFC.Api.Application.Enums;

namespace PalpiteFC.Api.Application.Requests;

public class WaitingListRequest
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Team { get; set; }
    public Gender? Gender { get; set; }
    public DateOnly? Birthday { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
}