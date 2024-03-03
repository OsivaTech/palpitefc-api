namespace PalpiteApi.Infra.Persistence.Entities;

public class ChampionshipTeamPoints
{
    public int Id { get; set; }
    public string? Position { get; set; }
    public int Points { get; set; }
    public int TeamId { get; set; }
    public int ChampionshipsId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
