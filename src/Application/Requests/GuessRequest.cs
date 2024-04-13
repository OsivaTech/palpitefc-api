namespace PalpiteFC.Api.Application.Requests;

public class GuessRequest
{
    public int GameId { get; set; }
    public PalpitationTeam? FirstTeam { get; set; }
    public PalpitationTeam? SecondTeam { get; set; }
}

public class PalpitationTeam
{
    public int Id { get; set; }
    public int Gol { get; set; }
}