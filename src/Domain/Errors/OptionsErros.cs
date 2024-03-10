using PalpiteApi.Domain.Result;

namespace PalpiteApi.Domain.Errors;
public static class OptionsErros
{
    public static readonly Error MissingParams = new("Options.MissingParams", "Missing Params");
    public static readonly Error OptionNotFound = new("Options.NotFound", "Option Not Found");
    public static readonly Error EnqueteNotFound = new("Options.EnqueteNotFound", "Enquete not found");
}
