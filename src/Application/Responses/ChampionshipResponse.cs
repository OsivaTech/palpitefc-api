namespace PalpiteFC.Api.Application.Responses;

public class ChampionshipResponse
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public IEnumerable<GameResponse>? Games { get; set; }
}
