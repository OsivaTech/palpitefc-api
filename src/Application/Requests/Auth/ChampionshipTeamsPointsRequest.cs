namespace PalpiteFC.Api.Application.Requests.Auth;

public class ChampionshipTeamsPointsRequest
{
    public int Id { get; set; }
    public int TeamId { get; set; }
    public int ChampionshipId { get; set; }
    public int Position { get; set; }
    public int Points { get; set; }
}

