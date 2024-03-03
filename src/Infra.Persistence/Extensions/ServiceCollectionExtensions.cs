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

        services.AddTransient<IVoteRepository, VoteRepository>();
    }
}
