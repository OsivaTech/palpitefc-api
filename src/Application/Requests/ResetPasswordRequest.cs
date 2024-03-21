namespace PalpiteFC.Api.Application.Requests;

public class ResetPasswordRequest
{
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? Code { get; set; }
}
