using PalpiteApi.Domain.Result;

namespace PalpiteApi.Domain.Errors;

public static class SignUpErrors
{
    public static readonly Message EmailAlreadyUsed = new("SignUp.UsedEmail", "This email is already in use.");
}
