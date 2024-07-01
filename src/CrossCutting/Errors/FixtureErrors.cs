using PalpiteFC.Api.CrossCutting.Result;

namespace PalpiteFC.Api.CrossCutting.Errors;

public static class FixtureErrors
{
    public static readonly Message FixtureNotFound = new("Fixture-NotFound", "This fixture was not found.");
}
