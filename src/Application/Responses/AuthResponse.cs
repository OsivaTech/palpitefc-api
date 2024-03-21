namespace PalpiteFC.Api.Application.Responses;

public sealed class AuthResponse
{
    public string? AccessToken { get; set; }
    public UserResponse? User { get; set; }
}
