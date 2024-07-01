using FluentValidation;
using PalpiteFC.Api.Application.Requests;

namespace PalpiteFC.Api.Application.Validators;

public class SubscriptionRequestValidator : AbstractValidator<SubscriptionRequest>
{
    public SubscriptionRequestValidator()
    {
        RuleFor(x => x).NotEmpty();
        RuleFor(x => x.Card).NotEmpty();
    }
}