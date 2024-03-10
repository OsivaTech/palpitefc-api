namespace PalpiteApi.Application.Requests.Auth;

public class NewsRequest
{
    public NewsData? News { get; set; }
}

public class NewsData
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Content { get; set; }
    public int UserId { get; set; }
}