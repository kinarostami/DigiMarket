using FluentValidation;

namespace Shop.Application.SiteEntities.ShippingMethod.Edit;

public class EditShippingMethodCommandValidator : AbstractValidator<EditShippingMethodCommand>
{
    public EditShippingMethodCommandValidator()
    {
        RuleFor(f => f.Title)
            .NotNull().NotEmpty();
    }
}