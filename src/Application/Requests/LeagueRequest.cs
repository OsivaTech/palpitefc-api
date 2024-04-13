namespace PalpiteFC.Api.Application.Requests;


public class LeagueRequest
{
    public Championship? Championship { get; set; }
}

public class Championship
{
    public int Id { get; set; }
    public string? Name { get; set; }
}
