namespace PalpiteApi.Domain.Entities;

public class Games : BaseEntity
{
    public string? Name { get; set; }
    public int ChampionshipId { get; set; }
    public DateTime Start { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
