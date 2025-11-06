using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastucture.Persistent.Ef;
using Shop.Query.Sellers.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Query.Sellers.Inventories.GetByProductId;

public record GetInventoryByProductIdQuery(long ProductId,long SellerId) : IQuery<List<InventoryDto>>
{
}

public class GetInventoryByProductIdQueryHandler : IQueryHandler<GetInventoryByProductIdQuery, List<InventoryDto>>
{
    private readonly ShopContext _context;

    public GetInventoryByProductIdQueryHandler(ShopContext context)
    {
        _context = context;
    }

    public async Task<List<InventoryDto>> Handle(GetInventoryByProductIdQuery request, CancellationToken cancellationToken)
    {
        var seller = _context.Sellers.Where(x => x.Id == request.SellerId).First();

        var product = _context.Products.Where(x => x.Id == request.ProductId).First();

        var inventory = await _context.Inventories
        .Where(i => i.ProductId == request.ProductId)
        .Select(i => new InventoryDto
        {
            Id = i.Id,
            SellerId = i.SellerId,
            ProductId = i.ProductId,
            Count = i.Count,
            Price = i.Price,
            CreationDate = i.CreationDate,
            DiscountPerentage = i.DiscountPercentage,
            ShopName = seller.ShopName,
            ProductTitle = product.Title,
            ProductImage = product.ImageName
        })
        .ToListAsync();

        return inventory;
    }
}
