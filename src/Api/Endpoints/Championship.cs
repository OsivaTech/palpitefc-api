﻿using PalpiteApi.Application.Services.Interfaces;

namespace PalpiteApi.Api.Endpoints;

public static class Championship
{
    public static void MapChampionshipEndpoints(this WebApplication app)
    {
        app.MapGet("/championship", async (IChampionshipsService service, CancellationToken cancellationToken) => await service.GetAsync(cancellationToken));
    }
}
