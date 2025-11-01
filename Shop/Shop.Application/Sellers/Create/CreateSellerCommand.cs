using Common.Application;
using Common.Application.Validation;
using Common.Application.Validation.FluentValidations;
using FluentValidation;
using Shop.Domain.SellerAgg;
using Shop.Domain.SellerAgg.Repository;
using Shop.Domain.SellerAgg.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Sellers.Create;

public class CreateSellerCommand : IBaseCommand
{
    public CreateSellerCommand(long userId, string shopName, string nationalCode)
    {
        UserId = userId;
        ShopName = shopName;
        NationalCode = nationalCode;
    }

    public long UserId { get; set; }
    public string ShopName { get; set; }
    public string NationalCode { get; set; }
}
public class CreateSellerCommandHandler : IBaseCommandHandler<CreateSellerCommand>
{
    private readonly ISellerRepository _sellerRepository;
    private readonly ISellerDomainService _domainService;

    public CreateSellerCommandHandler(ISellerRepository sellerRepository, ISellerDomainService domainService)
    {
        _sellerRepository = sellerRepository;
        _domainService = domainService;
    }

    public async Task<OperationResult> Handle(CreateSellerCommand request, CancellationToken cancellationToken)
    {
        var selelr = new Seller(request.UserId,request.ShopName,request.NationalCode,_domainService);
        _sellerRepository.Add(selelr);

        await _sellerRepository.Save();
        return OperationResult.Success();
    }
}
public class CreateSellerCommandValidator : AbstractValidator<CreateSellerCommand>
{
    public CreateSellerCommandValidator()
    {
        RuleFor(x => x.ShopName)
            .NotEmpty()
            .WithMessage(ValidationMessages.required("نام فروشگاه"));

        RuleFor(x => x.NationalCode)
            .NotEmpty()
            .WithMessage(ValidationMessages.required("کدملی"))
            .ValidNationalId();
    }
}