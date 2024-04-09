using PalpiteFC.Api.Integrations.ApiFootball.Responses;

namespace PalpiteFC.Api.Integrations.Interfaces;

public interface IApiFootballProvider
{
    Task<IEnumerable<Match>> GetMatchesByLeagueId(string leagueId, string fromDate, string toDate);
}