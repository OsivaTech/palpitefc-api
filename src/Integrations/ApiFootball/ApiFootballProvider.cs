using Microsoft.AspNetCore.Http.Extensions;
using PalpiteFC.Api.Integrations.ApiFootball.Requests;
using PalpiteFC.Api.Integrations.ApiFootball.Responses;
using PalpiteFC.Api.Integrations.Interfaces;
using System.Text.Json;

namespace PalpiteFC.Api.Integrations.ApiFootball;

public class ApiFootballProvider : IApiFootballProvider
{
    private static readonly JsonSerializerOptions _serializerOptions = new() { PropertyNameCaseInsensitive = true };
    private readonly HttpClient _httpClient;

    public ApiFootballProvider(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<LeagueResponse>> GetLeagues(LeaguesRequest request)
    {
        var queryBuilder = new QueryBuilder();

        if (request.Season > 0) queryBuilder.Add("season", request.Season.ToString());

        var uri = $"/v3/leagues{queryBuilder.ToQueryString()}";

        var response = await _httpClient.GetAsync(uri);

        var content = await response.Content.ReadAsStringAsync();

        var result = JsonSerializer.Deserialize<ApiFootballResult<LeagueResponse>>(content, _serializerOptions);

        return result!.Response!;
    }
}