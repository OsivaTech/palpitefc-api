using PalpiteFC.Api.Domain.Entities.ApiFootball;
using PalpiteFC.Api.Domain.Interfaces.Integrations;
using System.Text.Json;

namespace PalpiteFC.Api.Integrations.ApiFootball;

public class ApiFootballProvider : IApiFootballProvider
{
    private readonly HttpClient _httpClient;

    public ApiFootballProvider(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<Match>> GetMatchesByLeagueId(string leagueId, string fromDate, string toDate)
    {
        var uri = $"/v3/fixtures?league={leagueId}&season=2024&from={fromDate}&to={toDate}";

        var response = await _httpClient.GetAsync(uri);

        var content = await response.Content.ReadAsStringAsync();

        var result = JsonSerializer.Deserialize<ApiFootballResult<Match>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return result!.Response!;
    }
}
