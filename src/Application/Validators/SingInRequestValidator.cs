using FluentValidation;
using PalpiteFC.Api.Application.Requests;

namespace PalpiteFC.Api.Application.Validators;

public class SignInRequestValidator : AbstractValidator<SignInRequest>
{
    public SignInRequestValidator()
    {
        RuleFor(x => x).NotEmpty();
        RuleFor(x => x.Email).NotEmpty();
        RuleFor(x => x.Password).NotEmpty();
    }
}