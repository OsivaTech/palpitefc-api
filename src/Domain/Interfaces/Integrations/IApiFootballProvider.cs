using PalpiteFC.Api.Domain.Entities.ApiFootball;

namespace PalpiteFC.Api.Domain.Interfaces.Integrations;

public interface IApiFootballProvider
{
    Task<IEnumerable<Match>> GetMatchesByLeagueId(string leagueId, string fromDate, string toDate);
}