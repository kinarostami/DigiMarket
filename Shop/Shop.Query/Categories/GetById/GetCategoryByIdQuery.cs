using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastucture.Persistent.Ef;
using Shop.Query.Categories.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Query.Categories.GetById;

public record GetCategoryByIdQuery(long CategoryId) : IQuery<CategoryDto>
{
}
public class GetCategoryByIdQueryHandler : IQueryHandler<GetCategoryByIdQuery, CategoryDto>
{
    private readonly ShopContext _context;

    public GetCategoryByIdQueryHandler(ShopContext context)
    {
        _context = context;
    }

    public async Task<CategoryDto> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var model = await _context.Categories.FirstOrDefaultAsync(x => x.Id == request.CategoryId);

        return model.Map();
    }
}
