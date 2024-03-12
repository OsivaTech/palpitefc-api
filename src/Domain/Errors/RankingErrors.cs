using PalpiteApi.Domain.Result;

namespace PalpiteApi.Domain.Errors;
public static class RankingErrors
{
    public static readonly Message PointNotFound = new("Ranking.PointNotFound", "No one has point yet");

}
