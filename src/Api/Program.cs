using PalpiteFC.Api.Converters;
using PalpiteFC.Api.ExceptionHandlers;
using PalpiteFC.Api.Extensions;
using PalpiteFC.Api.Integrations.Extensions;
using PalpiteFC.Api.Middlewares;
using Serilog;
using System.Text.Json;
using System.Text.Json.Serialization;


try
{
    Log.Logger = new LoggerConfiguration()
        .WriteTo.Console(outputTemplate: "{Timestamp:dd-MM-yyyy HH:mm:ss} [{Level:u3}] [{ThreadId}] {Message}{NewLine}{Exception}")
        .CreateLogger();

    var builder = WebApplication.CreateBuilder(args);

    Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).CreateLogger();

    Log.Information("Configuring app.");

    builder.Host.UseSerilog(Log.Logger);

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("cors", policy =>
            {
                policy.AllowAnyOrigin()
                      .AllowAnyHeader()
                      .AllowAnyMethod();
            });
    });

    builder.Services.ConfigureHttpJsonOptions(options =>
    {
        options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.SerializerOptions.PropertyNameCaseInsensitive = true;
        options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.SerializerOptions.Converters.Add(new DateTimeConverter());
    });

    builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
    builder.Services.AddProblemDetails();

    builder.Services.ConfigureValidators();
    builder.Services.ConfigureDependencyInjection();
    builder.Services.AddIntegrationServices(builder.Configuration);
    builder.Services.ConfigureIOptions(builder.Configuration);
    builder.Services.ConfigureApiSecurity(builder.Configuration);

    var app = builder.Build();

    app.UseSerilogRequestLogging();

    app.UsePathBase(new PathString("/api"));
    app.UseRouting();

    app.UseCors("cors");

    app.UseAuthentication();
    app.UseAuthorization();

    await app.InitiaizeDatabase();
    app.UseMiddleware<UserContextMiddleware>();
    app.MapEndpoints();
    app.UseExceptionHandler();

    Log.Information("App configured. Starting...");

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly: {Message}", ex.Message);
}
finally
{
    Log.Information("Server Shutting down...");
    Log.CloseAndFlush();
}