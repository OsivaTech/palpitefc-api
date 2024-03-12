namespace PalpiteApi.Domain.Entities.Database;

public class Enquete
{
    public string? Title { get; set; }
    public IEnumerable<Options>? Options { get; set; }
}
