namespace PalpiteFC.Api.Application.Requests;

public class FixtureRequest
{
    public int? Id { get; set; }
    public string? Name { get; set; }
    public int ChampionshipId { get; set; }
    public bool Finished { get; set; }
    public Team? FirstTeam { get; set; }
    public Team? SecondTeam { get; set; }
}

public class Team
{
    public int Id { get; set; }
    public int Gol { get; set; }
}