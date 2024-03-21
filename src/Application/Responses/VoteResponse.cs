namespace PalpiteFC.Api.Application.Responses;

public class VoteResponse
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public IEnumerable<OptionsResponse>? Options { get; set; }
}
