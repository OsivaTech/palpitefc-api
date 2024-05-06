namespace PalpiteFC.Api.Application.Responses;

public class PointsResponse
{
    public int Points { get; set; }
    public DateTime Date { get; set; }
    public GuessResponse? Guess { get; set; }
    public FixtureResponse? Fixture { get; set; }
}
