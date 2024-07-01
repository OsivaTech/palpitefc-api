using PalpiteFC.Api.CrossCutting.Result;

namespace PalpiteFC.Api.CrossCutting.Errors;

public static class OtherErrors
{
    public static readonly Message StartDateLaterThanEnd = new("Other-StartDateLaterThanEnd", "The start date cannot be later then end date.");
}
