namespace PalpiteApi.Domain.Entities.Database;

public class News : BaseEntity
{
    public string? Title { get; set; }
    public string? Content { get; set; }
    public string? Info { get; set; }
    public int UserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
