using Microsoft.Extensions.DependencyInjection;
using PalpiteFC.Api.Domain.Interfaces;
using PalpiteFC.Api.Domain.Interfaces.Database;
using PalpiteFC.Api.Persistence.Connection;
using PalpiteFC.Api.Persistence.Repositories;

namespace PalpiteFC.Api.Persistence.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddDatabase(this IServiceCollection services)
    {
        services.AddScoped<DbSession>();
        services.AddSingleton<DataContext>();
        services.AddTransient<IUnitOfWork, UnitOfWork>();

        services.AddTransient<IVotesRepository, VotesRepository>();
        services.AddTransient<IOptionsRepository, OptionsRepository>();
        services.AddTransient<IChampionshipsRepository, ChampionshipsRepository>();
        services.AddTransient<IGamesRepository, GamesRepository>();
        services.AddTransient<ITeamsGamesRepository, TeamsGameRepository>();
        services.AddTransient<ITeamsRepository, TeamsRepository>();
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IPalpitationRepository, PalpitationRepository>();
        services.AddTransient<IConfigRepository, ConfigRepository>();
        services.AddTransient<INewsRepository, NewsRepository>();
        services.AddTransient<IChampionshipTeamPointsRepository, ChampionshipTeamPointsRepository>();
        services.AddTransient<IPointSeasonsRepository, PointSeasonsRepository>();
    }
}
