using PalpiteFC.Api.CrossCutting.Result;

namespace PalpiteFC.Api.CrossCutting.Errors;

public static class OptionErros
{
    public static readonly Message OptionNotFound = new("Option-NotFound", "This option was not found.");
}