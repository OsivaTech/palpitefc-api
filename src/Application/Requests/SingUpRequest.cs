using PalpiteFC.Api.Application.Contracts;
using PalpiteFC.Api.Application.Enums;

namespace PalpiteFC.Api.Application.Requests;

public sealed class SignUpRequest
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public Gender? Gender { get; set; }
    public string? Document { get; set; }
    public int TeamId { get; set; }
    public string? PhoneNumber { get; set; }
    public DateOnly? Birthday { get; set; }
    public Address? Address { get; set; }
}