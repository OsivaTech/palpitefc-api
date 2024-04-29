using PalpiteFC.Api.Application.Requests;

namespace PalpiteFC.Api.Application.Responses;

public class GuessResponse
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int FixtureId { get; set; }
    public GuessTeam? HomeTeam { get; set; }
    public GuessTeam? AwayTeam { get; set; }
}