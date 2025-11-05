using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastucture.Persistent.Ef;
using Shop.Query.Products.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Query.Products.GetById;

public record GetProductByIdQuery(long ProductId) : IQuery<ProductDto>
{
}
public class GetProductByIdQueryHandler : IQueryHandler<GetProductByIdQuery, ProductDto>
{
    private readonly ShopContext _shopContext;

    public GetProductByIdQueryHandler(ShopContext shopContext)
    {
        _shopContext = shopContext;
    }

    public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _shopContext.Products.FirstOrDefaultAsync(x => x.Id == request.ProductId);

        var model = product.Map();
        if (model == null)
            return null;
        await model.SetCategories(_shopContext);
        return model;
    }
}
