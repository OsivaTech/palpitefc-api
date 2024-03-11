using Infra.Integrations.ApiFootball;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PalpiteApi.Domain.Interfaces.Integrations;

namespace Infra.Integrations.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddIntegrationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient<IApiFootballProvider, ApiFootballProvider>(client =>
        {
            client.BaseAddress = new Uri(configuration["Settings:Integrations:ApiFootball:BaseAddress"]!);
            client.DefaultRequestHeaders.Add("X-RapidAPI-Host", configuration["Settings:Integrations:ApiFootball:Host"]!);
            client.DefaultRequestHeaders.Add("X-RapidAPI-Key", configuration["Settings:Integrations:ApiFootball:Key"]!);
        });
    }
}
