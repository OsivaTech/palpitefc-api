namespace PalpiteApi.Infra.Persistence.Entities;
public class Votes
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
