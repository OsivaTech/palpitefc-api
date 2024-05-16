using PalpiteFC.Api.Integrations.ApiFootball.Requests;
using PalpiteFC.Api.Integrations.ApiFootball.Responses;

namespace PalpiteFC.Api.Integrations.Interfaces;

public interface IApiFootballProvider
{
    Task<IEnumerable<LeagueResponse>> GetLeagues(LeaguesRequest request);
}