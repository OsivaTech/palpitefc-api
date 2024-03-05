using PalpiteApi.Domain.Result;

namespace PalpiteApi.Domain.Errors;

public static class SignInErrors
{
    public static readonly Error UserNotFound = new("SignIn.UserNotFound", "User not found for this email.");
    public static readonly Error IncorretPassword = new("SignIn.IncorretPassword", "Incorret password.");
}
