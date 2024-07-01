using PalpiteFC.Api.CrossCutting.Result;

namespace PalpiteFC.Api.CrossCutting.Errors;

public static class UserAddressErrors
{
    public static readonly Message ExistingAddress = new("UserAddress-ExistingAddress", "The user already has an address.");
}