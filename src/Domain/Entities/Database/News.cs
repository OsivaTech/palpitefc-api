namespace PalpiteFC.Api.Domain.Entities.Database;

public class News : BaseEntity
{
    public string? Title { get; set; }
    public string? Content { get; set; }
    public string? Info { get; set; }
    public int UserId { get; set; }
}
