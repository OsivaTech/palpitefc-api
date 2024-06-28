namespace PalpiteFC.Api.Application.Requests;

public class SubscriptionRequest
{
    public bool CreateCustomer { get; set; }
    public Card? Card { get; set; }
}