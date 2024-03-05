namespace PalpiteApi.Application.Responses;

public class NewsResponse
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Content { get; set; }
    public string? Info { get; set; }
    public int UserId { get; set; }
    public Author author { get; set; }

    public class Author 
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Team { get; set; }
    }
}