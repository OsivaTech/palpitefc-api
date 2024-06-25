using PalpiteFC.Api.Application.Enums;

namespace PalpiteFC.Api.Application.Responses;

public class PointsResponse
{
    public int FixtureId { get; set; }
    public int GuessId { get; set; }
    public DateTime Date { get; set; }
    public IEnumerable<Points>? Points { get; set; }
}

public class Points
{
    public int Value { get; set; }
    public PointType Type { get; set; }
}
