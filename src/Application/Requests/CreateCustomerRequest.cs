namespace PalpiteFC.Api.Application.Requests;

public class CreateCustomerRequest
{
    public Card? Card { get; set; }
}

public class Card
{
    public string? Encrypted { get; set; }
    public int SecurityCode { get; set; }
}