﻿using PalpiteFC.Api.CrossCutting.Result;

namespace PalpiteFC.Api.CrossCutting.Errors;

public static class SignUpErrors
{
    public static readonly Message EmailAlreadyUsed = new("SignUp-UsedEmail", "This email is already in use.");
}
