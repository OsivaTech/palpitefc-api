using FluentValidation;
using PalpiteFC.Api.Application.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PalpiteFC.Api.Application.Validators;
public class AdvertisementValidator : AbstractValidator<AdvertisementRequest>
{
    public AdvertisementValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required");

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Description is required");

        RuleFor(x => x.ImageBanner)
            .NotEmpty()
            .WithMessage("ImageBanner is required")
            .When(x => string.IsNullOrWhiteSpace(x.ImageCard));

        RuleFor(x => x.ImageCard)
            .NotEmpty()
            .WithMessage("ImageCard is required")
            .When(x => string.IsNullOrWhiteSpace(x.ImageBanner));

        RuleFor(x => x.Enabled)
            .NotEmpty()
            .WithMessage("Enabled is required");

        RuleFor(x => x.StartDate)
            .NotEmpty()
            .WithMessage("StartDate is required");

        RuleFor(x => x.EndDate)
            .NotEmpty()
            .WithMessage("EndDate is required");

        RuleFor(x => x.UrlGoTo)
            .NotEmpty()
            .WithMessage("UrlGoTo is required");
    }
}
