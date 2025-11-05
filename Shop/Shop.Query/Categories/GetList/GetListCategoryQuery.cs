using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastucture.Persistent.Ef;
using Shop.Query.Categories.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Query.Categories.GetList;

public class GetListCategoryQuery : IQuery<List<CategoryDto>>
{
}
public class GetListCategoryQueryHandler : IQueryHandler<GetListCategoryQuery, List<CategoryDto>>
{
    private readonly ShopContext _shopContext;

    public GetListCategoryQueryHandler(ShopContext shopContext)
    {
        _shopContext = shopContext;
    }

    public async Task<List<CategoryDto>> Handle(GetListCategoryQuery request, CancellationToken cancellationToken)
    {
        var result = await _shopContext.Categories
            .Where(x => x.ParentId == null)
            .Include(x => x.Child)
            .ThenInclude(x => x.Child)
            .OrderBy(x => x.Id)
            .ToListAsync();

        return result.Map();
    }
}
