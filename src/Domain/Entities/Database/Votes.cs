namespace PalpiteApi.Domain.Entities.Database;

public class Votes : BaseEntity
{
    public string? Title { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
