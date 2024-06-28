using PalpiteFC.Api.CrossCutting.Result;

namespace PalpiteFC.Api.CrossCutting.Errors;

public static class SubscriptionErrors
{
    public static readonly Message GenericError = new("Subscription.GenericError", "An error occurred while trying to proceed the subscription.");
}
