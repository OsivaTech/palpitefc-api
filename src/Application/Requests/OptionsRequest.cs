namespace PalpiteFC.Api.Application.Requests;

public class OptionsRequest
{
    public int Id { get; set; }
    public int PollId { get; set; }
    public string? Title { get; set; }
    public int Count { get; set; }
}
