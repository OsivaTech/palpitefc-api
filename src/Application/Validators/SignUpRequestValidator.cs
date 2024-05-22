using FluentValidation;
using PalpiteFC.Api.Application.Requests;

namespace PalpiteFC.Api.Application.Validators;

public class SignUpRequestValidator : AbstractValidator<SignUpRequest>
{
    public SignUpRequestValidator()
    {
        RuleFor(x => x).NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Email).NotEmpty();
        RuleFor(x => x.Password).NotEmpty();
        RuleFor(x => x.Gender).NotEmpty();
        RuleFor(x => x.Document).NotEmpty();
        RuleFor(x => x.TeamId).NotEmpty();
        RuleFor(x => x.PhoneNumber).NotEmpty();
        RuleFor(x => x.Birthday).NotEmpty();

        RuleFor(x => x.Address).SetValidator(new AddressValidator()!).When(x => x.Address is not null);
    }
}