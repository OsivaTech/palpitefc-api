namespace PalpiteFC.Api.Application.Responses;

public class TeamGameResponse
{
    public int Id { get; set; }
    public int Gol { get; set; }
    public int TeamId { get; set; }
    public int GameId { get; set; }
    public string? Name { get; set; }
    public string? Image { get; set; }
}
