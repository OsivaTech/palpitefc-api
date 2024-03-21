namespace PalpiteFC.Api.Application.Responses;

public class UserResponse
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public int Role { get; set; }
    public int Points { get; set; }
    public string? Team { get; set; }
    public string? Info { get; set; }
    public string? Number { get; set; }
    public string? Birthday { get; set; }
}
