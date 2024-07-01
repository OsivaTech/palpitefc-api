using PalpiteFC.Api.CrossCutting.Result;

namespace PalpiteFC.Api.CrossCutting.Errors;

public static class ResetPasswordErrors
{
    public static readonly Message InvalidCode = new("PasswordResetErrors-InvalidCode", "The verification code is invalid.");
}
