using Common.Application;
using Common.Application.Validation;
using Common.Domain.ValueObjects;
using FluentValidation;
using Shop.Domain.UserAgg;
using Shop.Domain.UserAgg.Repsitory;
using Shop.Domain.UserAgg.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Users.Register;

public class RegisterUserCommand : IBaseCommand
{
    public RegisterUserCommand(PhoneNumber phoneNumber, string password)
    {
        PhoneNumber = phoneNumber;
        Password = password;
    }

    public PhoneNumber PhoneNumber { get; set; }
    public string Password { get; set; }
}
public class RegisterUserCommandHandler : IBaseCommandHandler<RegisterUserCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserDomainService _userDomainService;

    public RegisterUserCommandHandler(IUserRepository userRepository, IUserDomainService userDomainService)
    {
        _userRepository = userRepository;
        _userDomainService = userDomainService;
    }

    public async Task<OperationResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = User.RegisterUser(request.PhoneNumber.Value,request.Password,_userDomainService);
        
        _userRepository.Add(user);
        await _userRepository.Save();
        return OperationResult.Success();
    }
}
public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage(ValidationMessages.required("کلمه عبور"))
            .NotNull().WithMessage(ValidationMessages.required("کلمه عبور"))
            .MinimumLength(4).WithMessage("کلمه عبور باید بیشتر از 4 کاراکتر باشد");
    }
}
