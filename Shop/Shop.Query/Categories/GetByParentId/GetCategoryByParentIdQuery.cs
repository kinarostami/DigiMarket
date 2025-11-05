using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastucture.Persistent.Ef;
using Shop.Query.Categories.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Query.Categories.GetByParentId;

public record GetCategoryByParentIdQuery(long ParentId) : IQuery<List<ChildCategoryDto>>
{
}
public class GetCategoryByParentIdQueryHandler : IQueryHandler<GetCategoryByParentIdQuery, List<ChildCategoryDto>>
{
    private readonly ShopContext _shopContext;

    public GetCategoryByParentIdQueryHandler(ShopContext shopContext)
    {
        _shopContext = shopContext;
    }

    public async Task<List<ChildCategoryDto>> Handle(GetCategoryByParentIdQuery request, CancellationToken cancellationToken)
    {
        var model = await _shopContext.Categories
            .Include(x => x.Child)
            .Where(x => x.ParentId == request.ParentId).ToListAsync();

        return model.MapChildren();
    }
}
