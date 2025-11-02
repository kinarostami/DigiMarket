using Common.Application.Validation;
using Common.Application.Validation.FluentValidations;
using FluentValidation;

namespace Shop.Application.SiteEntities.Slider.Edit;

public class EditSliderCommandValidator : AbstractValidator<EditSliderCommand>
{
    public EditSliderCommandValidator()
    {
        RuleFor(r => r.ImageFile)
            .NotNull().WithMessage(ValidationMessages.required("عکس"))
            .JustImageFile();

        RuleFor(r => r.Link)
            .NotNull()
            .NotEmpty().WithMessage(ValidationMessages.required("لینک"));

        RuleFor(r => r.Title)
            .NotNull()
            .NotEmpty().WithMessage(ValidationMessages.required("عنوان"));
    }
}