using PalpiteFC.Api.Domain.Result;

namespace PalpiteFC.Api.Domain.Errors;
public static class PalpitationErrors
{
    public static readonly Message PalpitationAlreadyExists = new("Palpitation.Exists", "There is already a palpitation for this game.");
}
