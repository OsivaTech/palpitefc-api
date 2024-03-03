using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace PalpiteApi.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddMediator(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
    }
}
