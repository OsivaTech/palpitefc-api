namespace PalpiteFC.Api.Application.Responses;

public class RankingResponse
{
    public RankingType Type { get; set; }
    public RankingInfo? Info { get; set; }
    public IEnumerable<UserPlacing>? Placings { get; set; }
    public UserPlacing? YourPlace { get; set; }

}

public class UserPlacing
{
    public int Id { get; set; }
    public int Place { get; set; }
    public string? Name { get; set; }
    public int Points { get; set; }
    public int Guesses { get; set; }
}

public enum RankingType
{
    League,
    Year,
    Month
}

public class RankingInfo
{
    public int? LeagueId { get; set; }
    public int? Month { get; set; }
    public int? Year { get; set; }
}