namespace PalpiteApi.Infra.Persistence.Entities;
public class Teams
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Image { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
