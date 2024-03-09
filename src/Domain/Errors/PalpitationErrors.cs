using PalpiteApi.Domain.Result;

namespace PalpiteApi.Domain.Errors;
public static class PalpitationErrors
{
    public static readonly Error PalpitationAlreadyExists = new("Palpitation.Exists", "There is already a palpitation for this game.");
}
