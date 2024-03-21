﻿using FluentValidation;
using PalpiteFC.Api.Application.Requests;

namespace PalpiteFC.Api.Application.Validators;
public class OptionsRequestValidator : AbstractValidator<OptionsRequest>
{
    public OptionsRequestValidator()
    {
        RuleFor(x => x.VoteId).NotEmpty().WithMessage("Id is required");
    }
}
