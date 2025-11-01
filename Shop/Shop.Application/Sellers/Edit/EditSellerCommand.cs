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

namespace Shop.Application.Sellers.Edit;

public class EditSellerCommand : IBaseCommand
{
    public long Id { get; set; }
    public string ShopName { get; set; }
    public string NationalCode { get; set; }
    public SellerStatus Status { get; set; }
}

public class EditSellerCommandHandler : IBaseCommandHandler<EditSellerCommand>
{
    private readonly ISellerRepository _sellerRepository;
    private readonly ISellerDomainService _sellerDomainService;

    public EditSellerCommandHandler(ISellerRepository sellerRepository, ISellerDomainService sellerDomainService)
    {
        _sellerRepository = sellerRepository;
        _sellerDomainService = sellerDomainService;
    }

    public async Task<OperationResult> Handle(EditSellerCommand request, CancellationToken cancellationToken)
    {
        var seller = await _sellerRepository.GetTracking(request.Id);
        if (seller == null)
            return OperationResult.NotFound();

        seller.Edit(request.ShopName,request.NationalCode,_sellerDomainService,request.Status);
        await _sellerRepository.Save();
        return OperationResult.Success();
    }
}
public class EditSellerCommandValidator : AbstractValidator<EditSellerCommand>
{
    public EditSellerCommandValidator()
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
