using Common.Application;
using Common.Application.Validation;
using Common.Domain.ValueObjects;
using FluentValidation;
using Shop.Domain.UserAgg;
using Shop.Domain.UserAgg.Repsitory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Users.AddAddress;

public class AddUserAddressCommand : IBaseCommand
{
    public AddUserAddressCommand(long userId, string shire, string city, string postalCode, string postalAddress,
       PhoneNumber phoneNumber, string name, string family, string nationalCode)
    {
        UserId = userId;
        Shire = shire;
        City = city;
        PostalCode = postalCode;
        PostalAddress = postalAddress;
        PhoneNumber = phoneNumber;
        Name = name;
        Family = family;
        NationalCode = nationalCode;
    }
    public long UserId { get; internal set; }
    public string Shire { get; set; }
    public string City { get; set; }
    public string PostalCode { get; set; }
    public string PostalAddress { get; set; }
    public PhoneNumber PhoneNumber { get; set; }
    public string Name { get; set; }
    public string Family { get; set; }
    public string NationalCode { get; set; }
    public bool ActiveAddress { get; set; }
}
public class AddUserAddressCommandHandler : IBaseCommandHandler<AddUserAddressCommand>
{
    private readonly IUserRepository _userRepository;

    public AddUserAddressCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<OperationResult> Handle(AddUserAddressCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetTracking(request.UserId);
        if (user == null)
            return OperationResult.NotFound();

        var address = new UserAddress(request.Shire, request.City, request.PostalCode, request.PostalAddress, request.PhoneNumber, request.Name, request.Family, request.NationalCode);
        user.AddAddress(address);

        await _userRepository.Save();
        return OperationResult.Success();
    }
}
public class AddUserAddressCommandValidator : AbstractValidator<AddUserAddressCommand>
{
    public AddUserAddressCommandValidator()
    {
        RuleFor(x => x.City)
            .NotEmpty().WithMessage(ValidationMessages.required("شهر"));

        RuleFor(x => x.Shire)
            .NotEmpty().WithMessage(ValidationMessages.required("استان"));

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage(ValidationMessages.required("نام"));

        RuleFor(x => x.Family)
            .NotEmpty().WithMessage(ValidationMessages.required("نام خانوادگی"));

        RuleFor(x => x.NationalCode)
            .NotEmpty().WithMessage(ValidationMessages.required("کدملی"));

        RuleFor(x => x.PostalAddress)
            .NotEmpty().WithMessage(ValidationMessages.required("آدرس پستی"));

        RuleFor(x => x.PostalCode)
            .NotEmpty().WithMessage(ValidationMessages.required("کد پستی"));

    }
}
