using PalpiteApi.Domain.Result;

namespace PalpiteApi.Domain.Errors;

public static class SignInErrors
{
    public static readonly Message UserNotFound = new("SignIn.UserNotFound", "User not found for this email.");
    public static readonly Message IncorretPassword = new("SignIn.IncorretPassword", "Incorret password.");
}
