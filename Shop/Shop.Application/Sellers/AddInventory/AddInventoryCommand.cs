using Common.Application;
using Shop.Domain.SellerAgg;
using Shop.Domain.SellerAgg.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Sellers.AddInventory;

public class AddInventoryCommand : IBaseCommand
{
    public AddInventoryCommand(long sellerId, long productId, int count, int price, int? discountPercentage)
    {
        SellerId = sellerId;
        ProductId = productId;
        Count = count;
        Price = price;
        DiscountPercentage = discountPercentage;
    }

    public long SellerId { get; internal set; }
    public long ProductId { get; private set; }
    public int Count { get; private set; }
    public int Price { get; private set; }
    public int? DiscountPercentage { get; private set; }
}

public class AddInventoryCommandHandler : IBaseCommandHandler<AddInventoryCommand>
{
    private readonly ISellerRepository _sellerRepository;

    public AddInventoryCommandHandler(ISellerRepository sellerRepository)
    {
        _sellerRepository = sellerRepository;
    }

    public async Task<OperationResult> Handle(AddInventoryCommand request, CancellationToken cancellationToken)
    {
        var seller = await _sellerRepository.GetTracking(request.SellerId);
        if (seller == null)
            return OperationResult.NotFound();

        var inventory = new SellerInventory(request.ProductId,request.Count,request.Price,request.DiscountPercentage);
        seller.AddInventory(inventory);

        await _sellerRepository.Save();
        return OperationResult.Success();
    }
}
