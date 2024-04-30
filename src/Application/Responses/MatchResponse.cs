namespace PalpiteFC.Api.Application.Responses;

public class MatchResponse
{
    public int Id { get; set; }
    public int Goals { get; set; }
    public int TeamId { get; set; }
    public int FixtureId { get; set; }
    public string? Name { get; set; }
    public string? Image { get; set; }
}
