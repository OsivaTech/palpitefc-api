namespace Application.Responses;

public class GameResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime Start { get; set; }
    public int ChampionshipId { get; set; }
    public bool? Finished { get; set; }
    public TeamGameResponse FirstTeam { get; set; }
    public TeamGameResponse SecondTeam { get; set; }
}
