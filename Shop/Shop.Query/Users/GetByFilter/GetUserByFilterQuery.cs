using Common.Query;
using Common.Query.Filter;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastucture.Persistent.Ef;
using Shop.Query.Users.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Query.Users.GetByFilter;

public class GetUserByFilterQuery : QueryFilter<UserFilterResult, UserFilterParams>, IQuery<UserFilterResult>
{
    public GetUserByFilterQuery(UserFilterParams filterParams) : base(filterParams)
    {
    }
}
public class GetUserByFilterQueryHandler : IQueryHandler<GetUserByFilterQuery, UserFilterResult>
{
    private readonly ShopContext _context;

    public GetUserByFilterQueryHandler(ShopContext context)
    {
        _context = context;
    }

    public async Task<UserFilterResult> Handle(GetUserByFilterQuery request, CancellationToken cancellationToken)
    {
        var param = request.FilterParams;
        var result = _context.Users.OrderByDescending(x => x.Id).AsQueryable();

        if (!string.IsNullOrWhiteSpace(param.PhoneNumber))
            result = result.Where(x => x.PhoneNumber.Contains(param.PhoneNumber));
        
        if (!string.IsNullOrWhiteSpace(param.Email))
            result = result.Where(x => x.Email.Contains(param.Email));
        
        if (param.Id != null)
            result = result.Where(x => x.Id == param.Id);

        var skip = (param.PageId - 1) * param.Take;

        var model = new UserFilterResult()
        {
            Data = await result.Skip(skip).Take(param.Take)
            .Select(x => x.MapFilterData()).ToListAsync(),
            FilterParams = param
        };
        model.GeneratePaging(result, param.Take, param.PageId);
        return model;
    }
}
