namespace PalpiteApi.Application.Responses;

public class ChampionshipResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<GameResponse> Games { get; set; }
}
