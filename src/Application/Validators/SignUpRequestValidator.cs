using FluentValidation;
using PalpiteApi.Application.Requests;

namespace PalpiteApi.Application.Validators;

public class SignUpRequestValidator : AbstractValidator<SignUpRequest>
{
    public SignUpRequestValidator()
    {
        RuleFor(x => x).NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Email).NotEmpty();
        RuleFor(x => x.Password).NotEmpty();
        RuleFor(x => x.Role).NotEmpty();
    }
}