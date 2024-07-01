using PalpiteFC.Api.CrossCutting.Result;

namespace PalpiteFC.Api.CrossCutting.Errors;
public static class RankingErrors
{
    public static readonly Message PointNotFound = new("Ranking-PointNotFound", "No one has point yet.");
}
