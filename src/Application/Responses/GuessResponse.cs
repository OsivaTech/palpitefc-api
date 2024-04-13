namespace PalpiteFC.Api.Application.Responses;

public class GuessResponse
{
    public int Id { get; set; }
    public int FirstTeamId { get; set; }
    public int FirstTeamGol { get; set; }
    public int SecondTeamId { get; set; }
    public int SecondTeamGol { get; set; }
    public int UserId { get; set; }
    public int GameId { get; set; }
}
