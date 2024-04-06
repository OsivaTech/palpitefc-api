namespace PalpiteFC.Api.Domain.Settings;

public class FixturesSettings
{
    public int DaysToSearch { get; set; }
    public string? CacheKey { get; set; }
    public TimeSpan? CacheExpiration { get; set; }
}
