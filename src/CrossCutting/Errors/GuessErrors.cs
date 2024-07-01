using PalpiteFC.Api.CrossCutting.Result;

namespace PalpiteFC.Api.CrossCutting.Errors;
public static class GuessErrors
{
    public static readonly Message GuessAlreadyExists = new("Guess-Exists", "There is already a guess for this match.");
    public static readonly Message MatchAlreadyStarted = new("Guess-StartedMatch", "This match already started, cannot send guesses.");
}
