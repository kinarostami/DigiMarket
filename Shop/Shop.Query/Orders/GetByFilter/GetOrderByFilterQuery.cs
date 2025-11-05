using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastucture.Persistent.Ef;
using Shop.Query.Orders.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Query.Orders.GetByFilter;

public class GetOrderByFilterQuery : QueryFilter<OrderFilterResult, OrderFilterParam>, IQuery<OrderFilterResult>
{
    public GetOrderByFilterQuery(OrderFilterParam filterParams) : base(filterParams)
    {
    }
}
public class GetOrderByFilterQueryHandler : IQueryHandler<GetOrderByFilterQuery, OrderFilterResult>
{
    private readonly ShopContext _context;

    public GetOrderByFilterQueryHandler(ShopContext context)
    {
        _context = context;
    }

    public async Task<OrderFilterResult> Handle(GetOrderByFilterQuery request, CancellationToken cancellationToken)
    {
        var param = request.FilterParams;
        var result = _context.Orders.OrderByDescending(x => x.Id).AsQueryable();

        if (param.Status != null)
            result = result.Where(x => x.Status == param.Status);

        if (param.UserId != null)
            result = result.Where(x => x.UserId == param.UserId);

        if (param.StartDate != null)
            result = result.Where(x => x.CreationDate.Date >= param.StartDate.Value.Date);
        
        if (param.EndDate != null)
            result = result.Where(x => x.CreationDate.Date <= param.EndDate.Value.Date);

        var skip = (param.PageId - 1) * param.Take;
        var model = new OrderFilterResult()
        {
            Data = await result.Skip(skip).Take(param.Take)
            .Select(x => x.MapFilterData(_context)).ToListAsync(),
            FilterParams = param,
        };
        model.GeneratePaging(result, param.Take, param.PageId);
        return model;
    }
}
