namespace PalpiteFC.Api.Application.Responses;

public class LeagueResponse
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public IEnumerable<FixtureResponse>? Games { get; set; }
}
