using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Domain.ProductAgg;
using Shop.Infrastucture.Persistent.Ef;
using Shop.Query.Products.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Query.Products.GetBySlug;

public record GetProductBySlugQuery(string Slug) : IQuery<ProductDto>
{
}

public class GetProductBySlugQueryHandler : IQueryHandler<GetProductBySlugQuery, ProductDto>
{
    private readonly ShopContext _context;

    public GetProductBySlugQueryHandler(ShopContext context)
    {
        _context = context;
    }

    public async Task<ProductDto> Handle(GetProductBySlugQuery request, CancellationToken cancellationToken)
    {
        var product = await _context.Products.FirstOrDefaultAsync(x => x.Slug == request.Slug);
        
        var modle = product.Map();
        if (modle == null)
            return null;
        await modle.SetCategories(_context);
        return modle;

    }
}
