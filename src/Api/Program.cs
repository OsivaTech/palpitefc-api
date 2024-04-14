using MassTransit;
using PalpiteFC.Api.Application.Requests;
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

    builder.Services.AddStackExchangeRedisCache(options =>
    {
        options.Configuration = builder.Configuration.GetValue<string>("Settings:Redis:ConnectionString");
    });

    builder.Services.AddMassTransit(x =>
    {
        x.UsingRabbitMq((context, cfg) =>
        {
            cfg.Host(builder.Configuration.GetValue<string>("Settings:RabbitMQ:Host"), "/", h =>
            {
                h.Username(builder.Configuration.GetValue<string>("Settings:RabbitMQ:Username"));
                h.Password(builder.Configuration.GetValue<string>("Settings:RabbitMQ:Password"));
            });

            cfg.Publish<GuessRequest>();
        });
    });

    var app = builder.Build();

    app.UseSerilogRequestLogging(opt => opt.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
    {
        var request = httpContext.Request;
        var response = httpContext.Response;

        diagnosticContext.Set("Host", request.Host);
        diagnosticContext.Set("IpAddress", httpContext.Connection.RemoteIpAddress);
        diagnosticContext.Set("Protocol", request.Protocol);
        diagnosticContext.Set("Scheme", request.Scheme);
        diagnosticContext.Set("QueryString", request.QueryString);
        diagnosticContext.Set("EndpointName", httpContext.GetEndpoint()?.DisplayName);

        diagnosticContext.Set("ContentType", response.ContentType);
    });

    app.UsePathBase(new PathString("/api"));
    app.UseRouting();

    app.UseCors("cors");

    app.UseAuthentication();
    app.UseAuthorization();

    await app.InitiaizeDatabase();
    app.UseMiddleware<HttpContextLoggerMiddleware>();
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