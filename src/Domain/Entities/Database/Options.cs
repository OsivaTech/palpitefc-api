namespace PalpiteApi.Domain.Entities.Database;

public class Options : BaseEntity
{
    public string? Title { get; set; }
    public int Count { get; set; }
    public int VoteId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
