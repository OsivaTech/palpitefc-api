﻿using PalpiteFC.Api.Domain.Result;

namespace PalpiteFC.Api.Domain.Errors;
public static class PointSeasonsErrors
{
    public static readonly Message ConflictDate = new("PointSeasons.ConflictDate", "There is already a season for this dates.");
    public static readonly Message PointSeasonNotFound = new("PointSeasons.PointSeasonNotFound", "No PointSeason was found for the current period.");
}
