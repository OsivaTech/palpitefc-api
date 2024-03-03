using PalpiteApi.Api.Endpoints;
using PalpiteApi.Api.ExceptionHandlers;
using PalpiteApi.Infra.Persistence.Connection;
using PalpiteApi.Infra.Persistence.Extensions;
using PalpiteApi.Infra.Persistence.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddDatabase();

builder.Services.Configure<DbSettings>(builder.Configuration.GetSection("Settings:Database:MySql"));

var app = builder.Build();

// initialize database
using var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<DataContext>();
await context.Init();

app.UseHttpsRedirection();
app.UseCors("cors");

app.MapVoteEndpoints();
app.MapChampionshipEndpoints();
app.MapGameEndpoints();

app.UseExceptionHandler();

app.Run();