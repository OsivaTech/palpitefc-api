using FluentValidation;
using PalpiteApi.Application.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PalpiteApi.Application.Validators;
public class OptionsRequestValidator : AbstractValidator<OptionsRequest>
{
    public OptionsRequestValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
    }
}
