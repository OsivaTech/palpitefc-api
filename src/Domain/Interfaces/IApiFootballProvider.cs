using PalpiteApi.Domain.Entities.ApiFootball;

namespace PalpiteApi.Domain.Interfaces;

public interface IApiFootballProvider
{
    Task<IEnumerable<Match>> GetMatchesByLeagueId(string leagueId, string fromDate, string toDate);
}