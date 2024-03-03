namespace PalpiteApi.Infra.Persistence.Entities;
public class Options
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public int Count { get; set; }
    public int VoteId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
