namespace PalpiteFC.Api.Application.Responses;


public class ChampoionshipTeamPointsResponse
{
    public int Id { get; set; }
    public int Position { get; set; }
    public int Points { get; set; }
    public int TeamId { get; set; }
    public int ChampionshipsId { get; set; }
    public TeamResponse? Team { get; set; }
    public ChampionshipResponse? Championships { get; set; }
}