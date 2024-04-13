using Microsoft.Extensions.DependencyInjection;
using PalpiteFC.Api.Application.Interfaces;
using PalpiteFC.Api.Application.Services;
using System.Security.Cryptography;

namespace PalpiteFC.Api.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IVotesService, VotesService>();
        services.AddScoped<IChampionshipsService, ChampionshipsService>();
        services.AddScoped<IGamesService, GamesService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IGuessService, GuessService>();
        services.AddScoped<IOptionsService, OptionsService>();
        services.AddScoped<IConfigService, ConfigService>();
        services.AddScoped<INewsService, NewsService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IRankingService, RankingService>();
        services.AddScoped<ITeamService, TeamService>();
        services.AddScoped<ITeamsPointsService, TeamsPointsService>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IPointSeasonsService, PointSeasonsService>();
        services.AddScoped<ICacheService, DistributedCacheService>();

        services.AddSingleton<IHashService, HashService>(_ => new HashService(SHA512.Create()));
    }
}
