using PalpiteFC.Api.CrossCutting.Result;

namespace PalpiteFC.Api.CrossCutting.Errors;

public static class PollErros
{
    public static readonly Message PollNotFound = new("Poll-NotFound", "Poll not found.");
    public static readonly Message UserAlreadyVoted = new("Poll-UserAlreadyVoted", "You have already voted in this poll.");
}

