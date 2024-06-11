using FluentValidation;
using PalpiteFC.Api.Application.Requests;

namespace PalpiteFC.Api.Application.Validators;

public class WaitingListRequestValidator : AbstractValidator<WaitingListRequest>
{
    public WaitingListRequestValidator()
    {
        RuleFor(x => x.City).NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Email).NotEmpty();
        RuleFor(x => x.Gender).NotEmpty();
        RuleFor(x => x.Team).NotEmpty();
        RuleFor(x => x.Birthday).NotEmpty();
        RuleFor(x => x.State).NotEmpty();
    }
}