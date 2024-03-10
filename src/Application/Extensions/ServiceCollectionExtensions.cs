using Microsoft.Extensions.DependencyInjection;
using PalpiteApi.Application.Services;
using PalpiteApi.Application.Services.Interfaces;

namespace PalpiteApi.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IVotesService, VotesService>();
        services.AddScoped<IChampionshipsService, ChampionshipsService>();
        services.AddScoped<IGamesService, GamesService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IOptionsService, OptionsService>();
    }
}
