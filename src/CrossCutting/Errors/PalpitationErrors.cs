﻿using PalpiteFC.Api.CrossCutting.Result;

namespace PalpiteFC.Api.CrossCutting.Errors;
public static class PalpitationErrors
{
    public static readonly Message PalpitationAlreadyExists = new("Palpitation.Exists", "There is already a palpitation for this game.");
}