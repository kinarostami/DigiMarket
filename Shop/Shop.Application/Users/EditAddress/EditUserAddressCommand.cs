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

namespace Shop.Application.Users.EditAddress;

public class EditUserAddressCommand : IBaseCommand
{
    public EditUserAddressCommand(long userId, string shire, string city, string postalCode, string postalAddress, PhoneNumber phoneNumber, string name, string family, string nationalCode, long id)
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
        Id = id;
    }
    public long UserId { get; set; }
    public long Id { get; set; }
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
public class EditUserAddressCommandHandler : IBaseCommandHandler<EditUserAddressCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserDomainService _userDomainService;

    public EditUserAddressCommandHandler(IUserRepository userRepository, IUserDomainService userDomainService)
    {
        _userRepository = userRepository;
        _userDomainService = userDomainService;
    }

    public async Task<OperationResult> Handle(EditUserAddressCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetTracking(request.UserId);
        if (user == null)
            return OperationResult.NotFound();

        var address = new UserAddress(request.Shire,request.City,request.PostalCode,request.PostalAddress,request.PhoneNumber,request.Name,request.Family,request.NationalCode);
        user.EditAddress(address, request.Id);

        await _userRepository.Save();
        return OperationResult.Success();
    }
}
public class EditUserAddressCommandValidator : AbstractValidator<EditUserAddressCommand>
{
    public EditUserAddressCommandValidator()
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
