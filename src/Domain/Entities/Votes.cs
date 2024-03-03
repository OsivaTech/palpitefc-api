namespace PalpiteApi.Domain.Entities;

public class Votes : BaseEntity
{
    public string? Title { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public IEnumerable<Options>? Options { get; set; }
}
