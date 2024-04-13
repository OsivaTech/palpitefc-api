using FluentValidation;
using PalpiteFC.Api.Application.Requests;

namespace PalpiteFC.Api.Application.Validators;

public class PalpitationRequestValidator : AbstractValidator<GuessRequest>
{
    public PalpitationRequestValidator()
    {
        RuleFor(x => x).NotEmpty();

        RuleFor(x => x.FirstTeam).NotEmpty();
        RuleFor(x => x.FirstTeam.Id).NotNull();
        RuleFor(x => x.FirstTeam.Gol).NotNull();

        RuleFor(x => x.SecondTeam).NotEmpty();
        RuleFor(x => x.SecondTeam.Id).NotNull();
        RuleFor(x => x.SecondTeam.Gol).NotNull();
    }
}
