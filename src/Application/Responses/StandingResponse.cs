namespace PalpiteFC.Api.Application.Responses;


public class StandingResponse
{
    public int Id { get; set; }
    public int Position { get; set; }
    public int Points { get; set; }
    public int TeamId { get; set; }
    public int LeagueId { get; set; }
    public TeamResponse? Team { get; set; }
    public LeagueResponse? League { get; set; }
}