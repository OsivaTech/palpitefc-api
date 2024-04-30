namespace PalpiteFC.Api.Application.Requests;

public class StandingRequest
{
    public int Id { get; set; }
    public int TeamId { get; set; }
    public int LeagueId { get; set; }
    public int Position { get; set; }
    public int Points { get; set; }
}

