using PalpiteFC.Api.Domain.Result;

namespace PalpiteFC.Api.Domain.Errors;

public static class ResetPasswordErrors
{
    public static readonly Message InvalidCode = new("PasswordResetErrors.InvalidCode", "The verification code is invalid.");
}
