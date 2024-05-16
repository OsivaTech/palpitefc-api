using PalpiteFC.Api.Integrations.ApiFootball.Responses.Entities;

namespace PalpiteFC.Api.Integrations.ApiFootball.Responses;

public class LeagueResponse
{
    public League? League { get; set; }
    public Country? Country { get; set; }
    public Season[]? Seasons { get; set; }
}