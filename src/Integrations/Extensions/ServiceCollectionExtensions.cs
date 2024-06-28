using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PalpiteFC.Api.Integrations.ApiFootball;
using PalpiteFC.Api.Integrations.Interfaces;
using PalpiteFC.Api.Integrations.PagBank;

namespace PalpiteFC.Api.Integrations.Extensions;

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

        services.AddHttpClient<IPagBankProvider, PagBankProvider>(client =>
        {
            client.BaseAddress = new Uri("https://api.mercadopago.com");
            client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "Bearer TEST-788839605460239-062208-feea299b5b85a8bf404897d6bf05d5ff-165347412");
        });
    }
}
