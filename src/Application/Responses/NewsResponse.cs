namespace PalpiteFC.Api.Application.Responses;

public class NewsResponse
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Content { get; set; }
    public string? Info { get; set; }
    public int UserId { get; set; }
    public AuthorInfo? Author { get; set; }

    public class AuthorInfo
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Team { get; set; }
    }
}