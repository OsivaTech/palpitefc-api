namespace PalpiteApi.Infra.Persistence.Entities;
public class Games
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int ChampionshipId { get; set; }
    public DateTime Start { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
