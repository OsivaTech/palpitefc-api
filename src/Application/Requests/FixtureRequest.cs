namespace PalpiteFC.Api.Application.Requests;

public class FixtureRequest
{
    public int? Id { get; set; }
    public string? Name { get; set; }
    public int LeagueId { get; set; }
    public bool Finished { get; set; }
    public Team? HomeTeam { get; set; }
    public Team? AwayTeam { get; set; }
}

public class Team
{
    public int Id { get; set; }
    public int Goals { get; set; }
}