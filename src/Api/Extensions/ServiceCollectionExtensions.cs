using FluentValidation;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using PalpiteFC.Api.Application.Extensions;
using PalpiteFC.Api.Application.Mappings;
using PalpiteFC.Api.Application.Utils;
using PalpiteFC.Api.Application.Validators;
using PalpiteFC.Api.CrossCutting.Settings;
using PalpiteFC.Libraries.Persistence.Database.Extensions;
using PalpiteFC.Libraries.Persistence.Database.Settings;
using System.Text;

namespace PalpiteFC.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static void ConfigureDependencyInjection(this IServiceCollection services)
    {
        services.AddServices();
        services.AddDatabase();
        services.AddCustomMappings();

        services.AddScoped<UserContext>();
    }

    public static void ConfigureApiSecurity(this IServiceCollection services, IConfiguration configuration)
    {
        var key = Encoding.ASCII.GetBytes(configuration["Settings:Jwt:SecurityKey"]!);

        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
        {
            x.RequireHttpsMetadata = false;
            x.SaveToken = true;
            x.TokenValidationParameters = new()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
            };
        });

        services.AddAuthorizationBuilder().AddPolicy("user", policy => policy.RequireRole("300"));
        services.AddAuthorizationBuilder().AddPolicy("journalist", policy => policy.RequireRole("200"));
        services.AddAuthorizationBuilder().AddPolicy("admin", policy => policy.RequireRole("100"));
    }

    public static void ConfigureValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<SignUpRequestValidator>(ServiceLifetime.Transient);
    }

    public static void ConfigureIOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DbSettings>(configuration.GetSection("Settings:Database:MySql"));
        services.Configure<JwtSettings>(configuration.GetSection("Settings:Jwt"));
        services.Configure<MailingSettings>(configuration.GetSection("Settings:Mailing"));
        services.Configure<FixturesSettings>(configuration.GetSection("Settings:Fixtures"));
    }

    private static void AddCustomMappings(this IServiceCollection services)
    {
        var typeAdapterConfig = TypeAdapterConfig.GlobalSettings;

        typeAdapterConfig.Scan(typeof(MappingConfiguration).Assembly);
        typeAdapterConfig.Compile();

        var mapperConfig = new Mapper(typeAdapterConfig);

        services.AddSingleton<IMapper>(mapperConfig);
    }
}
