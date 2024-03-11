namespace PalpiteApi.Domain.Entities.Database;

public class Championships : BaseEntity
{
    public string? Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
