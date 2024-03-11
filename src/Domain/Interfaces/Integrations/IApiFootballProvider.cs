using PalpiteApi.Domain.Entities.ApiFootball;

namespace PalpiteApi.Domain.Interfaces.Integrations;

public interface IApiFootballProvider
{
    Task<IEnumerable<Match>> GetMatchesByLeagueId(string leagueId, string fromDate, string toDate);
}