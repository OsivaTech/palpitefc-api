using Mapster;
using MapsterMapper;
using PalpiteApi.Application.Mappings;

namespace PalpiteApi.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddCustomMappings(this IServiceCollection services)
    {
        var typeAdapterConfig = TypeAdapterConfig.GlobalSettings;

        typeAdapterConfig.Scan(typeof(MappingConfiguration).Assembly);
        typeAdapterConfig.Compile();

        var mapperConfig = new Mapper(typeAdapterConfig);

        services.AddSingleton<IMapper>(mapperConfig);
    }
}
