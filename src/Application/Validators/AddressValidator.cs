using FluentValidation;
using PalpiteFC.Api.Application.Responses;

namespace PalpiteFC.Api.Application.Validators;

public class AddressValidator : AbstractValidator<AddressResponse>
{
    public AddressValidator()
    {
        RuleFor(x => x.Street).NotEmpty();
        RuleFor(x => x.Number).NotEmpty();
        RuleFor(x => x.Neighborhood).NotEmpty();
        RuleFor(x => x.City).NotEmpty();
        RuleFor(x => x.State).NotEmpty();
        RuleFor(x => x.Country).NotEmpty();
        RuleFor(x => x.PostalCode).NotEmpty();
    }
}