using PalpiteFC.Api.CrossCutting.Result;

namespace PalpiteFC.Api.CrossCutting.Errors;

public static class ConfigErrors
{
    public static readonly Message NotExists = new("Config-NotExists", "This config does not exists.");
    public static readonly Message AlreadyExists = new("Config-AlreadyExists", "This config already exists.");
}
