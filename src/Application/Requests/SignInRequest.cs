namespace PalpiteFC.Api.Application.Requests;

public sealed class SignInRequest
{
    public string? Email { get; set; }
    public string? Password { get; set; }
}
