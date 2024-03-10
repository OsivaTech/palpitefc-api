namespace PalpiteApi.Application.Services;


public class ChampionshipRequest
{
    public Championship? Championship { get; set; }
}

public class Championship
{
    public int Id { get; set; }
    public string? Name { get; set; }
}
