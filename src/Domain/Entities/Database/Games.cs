namespace PalpiteApi.Domain.Entities.Database;

public class Games : BaseEntity
{
    public string? Name { get; set; }
    public int ChampionshipId { get; set; }
    public DateTime Start { get; set; }
    public bool Finished { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
