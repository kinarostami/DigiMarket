using Common.Application.Validation;
using Common.Application.Validation.FluentValidations;
using FluentValidation;

namespace Shop.Application.Users.Create;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.PhoneNumber).ValidPhoneNumber();

        RuleFor(x => x.Email).EmailAddress().WithMessage("ایمیل نامعتبر است");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage(ValidationMessages.required("کلمه عبور"))
            .NotNull().WithMessage(ValidationMessages.required("کلمه عبور"))
            .MinimumLength(4).WithMessage("کلمه عبور باید بیشتر از 4 کاراکتر باشد");
    }
}