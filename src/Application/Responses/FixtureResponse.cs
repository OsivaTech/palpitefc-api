namespace PalpiteFC.Api.Application.Responses;

public class FixtureResponse
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public DateTime? Start { get; set; }
    public int LeagueId { get; set; }
    public bool Finished { get; set; }
    public MatchResponse? HomeTeam { get; set; }
    public MatchResponse? AwayTeam { get; set; }
}
