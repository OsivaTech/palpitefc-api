namespace PalpiteFC.Api.Application.Requests;

public class GuessRequest
{
    public int FixtureId { get; set; }
    public GuessTeam? HomeTeam { get; set; }
    public GuessTeam? AwayTeam { get; set; }
}

public class GuessTeam
{
    public int Id { get; set; }
    public int Goals { get; set; }
}