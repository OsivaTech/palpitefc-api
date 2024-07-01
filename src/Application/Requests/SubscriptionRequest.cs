namespace PalpiteFC.Api.Application.Requests;

public class SubscriptionRequest
{
    public Card? Card { get; set; }
    public bool CreateCustomer { get; set; }
}