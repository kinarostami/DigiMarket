using Common.Query;
using Common.Query.Filter;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastucture.Persistent.Ef;
using Shop.Query.Sellers.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Query.Sellers.GetByFilter;

public class GetSellerByFilterQuery : QueryFilter<SellerFilterResult, SellerFilterParams>, IQuery<SellerFilterResult>
{
    public GetSellerByFilterQuery(SellerFilterParams filterParams) : base(filterParams)
    {
    }
}

public class GetSellerByFilterQueryHandler : IQueryHandler<GetSellerByFilterQuery, SellerFilterResult>
{
    private readonly ShopContext _context;

    public GetSellerByFilterQueryHandler(ShopContext context)
    {
        _context = context;
    }

    public async Task<SellerFilterResult> Handle(GetSellerByFilterQuery request, CancellationToken cancellationToken)
    {
        var param = request.FilterParams;
        var result = _context.Sellers.OrderByDescending(x => x.Id).AsQueryable();

        if(!string.IsNullOrWhiteSpace(param.NationalCode))
            result = result.Where(x => x.NationalCode.Contains(param.NationalCode));

        if(!string.IsNullOrWhiteSpace(param.ShopName))
            result = result.Where(x => x.ShopName.Contains(param.ShopName));

        var skip = (param.PageId - 1) * param.Take;

        var model = new SellerFilterResult()
        {
            Data = await result.Skip(skip).Take(param.Take)
            .Select(x => x.Map())
            .ToListAsync(),
            FilterParams = param
        };
        model.GeneratePaging(result, param.Take, param.PageId);
        return model;
    }
}
