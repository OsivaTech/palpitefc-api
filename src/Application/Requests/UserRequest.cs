namespace PalpiteApi.Application.Requests;

public class UserRequest
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public int Role { get; set; }
    public int Points { get; set; }
    public string? Team { get; set; }
    public string? Document { get; set; }
    public string? Birthday { get; set; }
}

