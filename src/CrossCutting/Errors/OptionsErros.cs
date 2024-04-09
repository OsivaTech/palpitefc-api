﻿using PalpiteFC.Api.CrossCutting.Result;

namespace PalpiteFC.Api.CrossCutting.Errors;
public static class OptionsErros
{
    public static readonly Message MissingParams = new("Options.MissingParams", "Missing Params");
    public static readonly Message OptionNotFound = new("Options.NotFound", "Option Not Found");
    public static readonly Message EnqueteNotFound = new("Options.EnqueteNotFound", "Enquete not found");
}