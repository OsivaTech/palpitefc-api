using PalpiteApi.Domain.Result;

namespace PalpiteApi.Domain.Errors;

public static class ResetPasswordErrors
{
    public static readonly Message InvalidCode = new("PasswordResetErrors.InvalidCode", "The verification code is invalid.");
}
