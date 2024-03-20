namespace PalpiteApi.Application.Requests;

public sealed class SignUpRequest
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public int Role { get; set; }
    public string? Points { get; set; }
    public string? Document { get; set; }
    public string? Team { get; set; }
    public string? Info { get; set; }
    public string? Number { get; set; }
    public string? Birthday { get; set; }
    public string? Code { get; set; }
}