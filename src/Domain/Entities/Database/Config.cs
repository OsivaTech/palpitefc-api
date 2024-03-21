namespace PalpiteFC.Api.Domain.Entities.Database;

public class Config : BaseEntity
{
    public string? Name { get; set; }
    public string? Value { get; set; }
}
