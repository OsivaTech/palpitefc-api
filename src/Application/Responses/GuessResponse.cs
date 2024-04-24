namespace PalpiteFC.Api.Application.Responses;

public class GuessResponse
{
    public int Id { get; set; }
    public int HomeTeamId { get; set; }
    public int HomeTeamGoal { get; set; }
    public int AwayTeamId { get; set; }
    public int AwayTeamGoal { get; set; }
    public int UserId { get; set; }
    public int FixtureId { get; set; }
}
