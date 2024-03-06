using PalpiteApi.Api.Converters;
using PalpiteApi.Api.ExceptionHandlers;
using PalpiteApi.Api.Extensions;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

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
builder.Services.ConfigureIOptions(builder.Configuration);
builder.Services.ConfigureApiSecurity(builder.Configuration);

var app = builder.Build();

app.UsePathBase(new PathString("/api"));
app.UseRouting();

app.UseHttpsRedirection();
app.UseCors("cors");

app.UseAuthentication();
app.UseAuthorization();

app.InitiaizeDatabase();
app.MapEndpoints();
app.UseExceptionHandler();

app.Run();