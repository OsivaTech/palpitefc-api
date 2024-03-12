namespace PalpiteApi.Application.Responses;

public class PalpitationResponse
{
    public int Id { get; set; }
    public int FirstTeamId { get; set; }
    public int FirstTeamGol { get; set; }
    public int SecondTeamId { get; set; }
    public int SecondTeamGol { get; set; }
    public int UserId { get; set; }
    public int GameId { get; set; }
}
