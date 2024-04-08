using PalpiteFC.Api.Domain.Result;

namespace PalpiteFC.Api.Domain.Errors;
public static class RankingErrors
{
    public static readonly Message PointNotFound = new("Ranking.PointNotFound", "No one has point yet.");
}
