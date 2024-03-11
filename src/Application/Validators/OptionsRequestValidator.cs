using FluentValidation;
using PalpiteApi.Application.Requests;

namespace PalpiteApi.Application.Validators;
public class OptionsRequestValidator : AbstractValidator<OptionsRequest>
{
    public OptionsRequestValidator()
    {
        RuleFor(x => x.VoteId).NotEmpty().WithMessage("Id is required");
    }
}
