using FluentValidation;
using PalpiteFC.Api.Application.Requests;

namespace PalpiteFC.Api.Application.Validators;

public class GuessRequestValidator : AbstractValidator<GuessRequest>
{
    public GuessRequestValidator()
    {
        RuleFor(x => x).NotEmpty();

        RuleFor(x => x.HomeTeam).NotEmpty();
        RuleFor(x => x.HomeTeam.Id).NotNull();
        RuleFor(x => x.HomeTeam.Goal).NotNull();

        RuleFor(x => x.AwayTeam).NotEmpty();
        RuleFor(x => x.AwayTeam.Id).NotNull();
        RuleFor(x => x.AwayTeam.Goal).NotNull();
    }
}
