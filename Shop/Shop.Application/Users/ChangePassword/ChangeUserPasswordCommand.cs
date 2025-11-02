using Common.Application;
using Common.Application.SecurityUtil;
using Common.Application.Validation;
using FluentValidation;
using Shop.Domain.UserAgg.Repsitory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Users.ChangePassword;

public class ChangeUserPasswordCommand : IBaseCommand
{
    public long UserId { get; set; }
    public string CurrentPassword { get; set; }
    public string Password { get; set; }
}
public class ChangeUserPasswordCommandHandler : IBaseCommandHandler<ChangeUserPasswordCommand>
{
    private readonly IUserRepository _userRepository;

    public ChangeUserPasswordCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<OperationResult> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetTracking(request.UserId);
        if (user == null)
            return OperationResult.NotFound("کاربر یافت نشد");

        var currentPassword = Sha256Hasher.Hash(request.CurrentPassword);
        if (user.Password != currentPassword)
            return OperationResult.Error("کلمه عبور فعلی معتبر نیست");

        var newPassword = Sha256Hasher.Hash(request.Password);
        user.ChangePassword(newPassword);

        await _userRepository.Save();
        return OperationResult.Success();
    }
}
public class ChangeUserPasswordCommandValidator : AbstractValidator<ChangeUserPasswordCommand>
{
    public ChangeUserPasswordCommandValidator()
    {
        RuleFor(r => r.CurrentPassword)
            .NotEmpty().WithMessage(ValidationMessages.required("کلمه عبور فعلی"))
            .MinimumLength(5).WithMessage(ValidationMessages.required("کلمه عبور فعلی"));

        RuleFor(r => r.Password)
            .NotEmpty().WithMessage(ValidationMessages.required("کلمه عبور فعلی"))
            .MinimumLength(5).WithMessage(ValidationMessages.required("کلمه عبور فعلی"));
    }
}
