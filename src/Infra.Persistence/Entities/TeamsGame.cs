namespace PalpiteApi.Infra.Persistence.Entities;
public class TeamsGame
{
    public int Id { get; set; }
    public int Gol { get; set; }
    public int TeamId { get; set; }
    public int GameId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
