using Common.Query;
using Common.Query.Filter;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastucture.Persistent.Ef;
using Shop.Query.Products.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Query.Products.GetByFilter;

public class GetProductByFilterQuery : QueryFilter<ProductFilterResult, ProductFilterParams>, IQuery<ProductFilterResult>
{
    public GetProductByFilterQuery(ProductFilterParams filterParams) : base(filterParams)
    {
    }
}

public class GetProductByFilterQueryHandler : IQueryHandler<GetProductByFilterQuery, ProductFilterResult>
{
    private readonly ShopContext _shopContext;

    public GetProductByFilterQueryHandler(ShopContext shopContext)
    {
        _shopContext = shopContext;
    }

    public async Task<ProductFilterResult> Handle(GetProductByFilterQuery request, CancellationToken cancellationToken)
    {
        var param = request.FilterParams;
        var result = _shopContext.Products.OrderByDescending(x => x.Id).AsQueryable();

        if(!string.IsNullOrWhiteSpace(param.Slug))
            result = result.Where(x => x.Slug.Contains(param.Slug));

        if(!string.IsNullOrWhiteSpace(param.Title))
            result = result.Where(x => x.Title.Contains(param.Title));

        if(param.Id != null)
            result = result.Where(x => x.Id == param.Id);

        var skip = (param.PageId - 1) * param.Take;
        var model = new ProductFilterResult()
        {
            Data = await result.Skip(skip).Take(param.Take).Select(s => s.MapListData())
                .ToListAsync(),
            FilterParams = param
        };
        model.GeneratePaging(result, param.Take, param.PageId);
        return model;
    }
}
