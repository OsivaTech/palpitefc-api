using PalpiteFC.Api.Domain.Result;

namespace PalpiteFC.Api.Domain.Errors;
public static class PointSeasonsErrors
{
    public static readonly Message ConflictDate = new("PointSeasons.ConflictDate", "There is already a season for this dates.");
}
