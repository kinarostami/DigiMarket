using FluentValidation;

namespace Shop.Application.SiteEntities.ShippingMethod.Create;

public class CreateShippingMethodCommandValidator : AbstractValidator<CreateShippingMethodCommand>
{
    public CreateShippingMethodCommandValidator()
    {
        RuleFor(f => f.Title)
            .NotNull().NotEmpty();
    }
}