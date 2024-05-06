using Microsoft.Extensions.DependencyInjection;
using PalpiteFC.Api.Application.Interfaces;
using PalpiteFC.Api.Application.Services;
using System.Security.Cryptography;

namespace PalpiteFC.Api.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IPollService, PollService>();
        services.AddScoped<ILeagueService, LeagueService>();
        services.AddScoped<IFixtureService, FixtureService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IGuessService, GuessService>();
        services.AddScoped<IConfigService, ConfigService>();
        services.AddScoped<INewsService, NewsService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IRankingService, RankingService>();
        services.AddScoped<ITeamService, TeamService>();
        services.AddScoped<IStandingService, StandingService>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IPointSeasonsService, PointSeasonsService>();
        services.AddScoped<IPointsService, PointsService>();

        services.AddScoped<ICacheService, DistributedCacheService>();
        services.AddSingleton<IHashService, HashService>(_ => new HashService(SHA512.Create()));
    }
}
