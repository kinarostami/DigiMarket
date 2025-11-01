using Common.Application;
using Shop.Domain.SellerAgg.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Sellers.EditInventory;

public class EditInventoryCommand : IBaseCommand
{
    public long SellerId { get; internal set; }
    public long InventoryId { get; private set; }
    public int Count { get; private set; }
    public int Price { get; private set; }
    public int? DiscountPercentage { get; private set; }
}

public class EditInventoryCommandHandler : IBaseCommandHandler<EditInventoryCommand>
{
    private readonly ISellerRepository _sellerRepository;

    public EditInventoryCommandHandler(ISellerRepository sellerRepository)
    {
        _sellerRepository = sellerRepository;
    }

    public async Task<OperationResult> Handle(EditInventoryCommand request, CancellationToken cancellationToken)
    {
        var seller = await _sellerRepository.GetTracking(request.SellerId);
        if (seller == null)
            return OperationResult.NotFound();

        seller.EditInventory(request.InventoryId, request.Count, request.Price, request.DiscountPercentage);

        await _sellerRepository.Save();
        return OperationResult.Success();
    }
}
