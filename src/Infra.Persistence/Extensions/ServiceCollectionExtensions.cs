using Microsoft.Extensions.DependencyInjection;
using PalpiteApi.Domain.Interfaces;
using PalpiteApi.Infra.Persistence.Connection;
using PalpiteApi.Infra.Persistence.Repositories;

namespace PalpiteApi.Infra.Persistence.Extensions;

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
    }
}
