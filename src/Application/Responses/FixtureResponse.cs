namespace PalpiteFC.Api.Application.Responses;

public class FixtureResponse
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public DateTime? Start { get; set; }
    public int ChampionshipId { get; set; }
    public bool Finished { get; set; }
    public MatchResponse? FirstTeam { get; set; }
    public MatchResponse? SecondTeam { get; set; }
}
