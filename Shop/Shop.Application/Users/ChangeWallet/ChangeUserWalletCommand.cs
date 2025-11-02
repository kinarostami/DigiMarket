using Common.Application;
using Common.Application.Validation;
using FluentValidation;
using Shop.Domain.UserAgg;
using Shop.Domain.UserAgg.Repsitory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Users.ChangeWallet;

public class ChangeUserWalletCommand : IBaseCommand
{
    public ChangeUserWalletCommand(long userId, int price, string description, bool isFinally, WalletType type)
    {
        UserId = userId;
        Price = price;
        Description = description;
        IsFinally = isFinally;
        Type = type;
    }

    public long UserId { get; internal set; }
    public int Price { get; private set; }
    public string Description { get; private set; }
    public bool IsFinally { get; private set; }
    public WalletType Type { get; private set; }
}
public class ChangeUserWalletCommandHandler : IBaseCommandHandler<ChangeUserWalletCommand>
{
    private readonly IUserRepository _userRepository;

    public ChangeUserWalletCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<OperationResult> Handle(ChangeUserWalletCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetTracking(request.UserId);
        if (user == null)
            return OperationResult.NotFound("کاربر یافت نشد");

        var wallet = new Wallet(request.UserId,request.Price,request.Description,request.IsFinally,request.Type);
        user.ChangeWallet(wallet);

        await _userRepository.Save();
        return OperationResult.Success();
    }
}
public class ChargeUserWalletCommandValidator : AbstractValidator<ChangeUserWalletCommand>
{
    public ChargeUserWalletCommandValidator()
    {
        RuleFor(x => x.Description).NotNull().WithMessage(ValidationMessages.required("توضیحات"));

        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(1000);
    }
}
