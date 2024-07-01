using PalpiteFC.Api.CrossCutting.Result;

namespace PalpiteFC.Api.CrossCutting.Errors;

public static class UserErrors
{
    public static readonly Message InvalidEmail = new("User-InvalidEmail", "User email is invalid.");
}