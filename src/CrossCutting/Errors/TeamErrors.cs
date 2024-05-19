using PalpiteFC.Api.CrossCutting.Result;

namespace PalpiteFC.Api.CrossCutting.Errors;

public static class TeamErrors
{
    public static readonly Message InvalidTeamId = new("Team.InvalidTeamId", "This team id is invalid.");
}