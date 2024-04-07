namespace PalpiteFC.Api.Domain.Entities.Database;

public class Games : BaseEntity
{
    public string? Name { get; set; }
    public int ChampionshipId { get; set; }
    public DateTime Start { get; set; }
    public bool Finished { get; set; }
}
