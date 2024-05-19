using PalpiteFC.Api.Application.Contracts;
using PalpiteFC.Api.Application.Enums;

namespace PalpiteFC.Api.Application.Responses;

public class UserResponse
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public Genre? Genre { get; set; }
    public string? Document { get; set; }
    public string? PhoneNumber { get; set; }
    public DateOnly? Birthday { get; set; }
    public int Role { get; set; }
    public TeamResponse? Team { get; set; }
    public Address? Address { get; set; }
}