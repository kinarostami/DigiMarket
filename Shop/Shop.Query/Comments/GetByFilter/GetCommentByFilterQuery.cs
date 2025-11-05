using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastucture.Persistent.Ef;
using Shop.Query.Comments.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Query.Comments.GetByFilter;

public class GetCommentByFilterQuery : QueryFilter<CommentFilterResult, CommentFilterParams>, IQuery<CommentFilterResult>
{
    public GetCommentByFilterQuery(CommentFilterParams filterParams) : base(filterParams)
    {
    }
}

public class GetCommentByFilterQueryHandler : IQueryHandler<GetCommentByFilterQuery, CommentFilterResult>
{
    private readonly ShopContext _context;

    public GetCommentByFilterQueryHandler(ShopContext context)
    {
        _context = context;
    }

    public async Task<CommentFilterResult> Handle(GetCommentByFilterQuery request, CancellationToken cancellationToken)
    {
        var @param = request.FilterParams;

        var result = _context.Comments.OrderByDescending(x => x.CreationDate).AsQueryable();

        if(@param.ProductId != null)
            result = result.Where(x => x.ProductId == request.FilterParams.ProductId);

        if(@param.UserId != null)
            result = result.Where(x => x.UserId == request.FilterParams.UserId);
        
        if(@param.CommentStatus != null)
            result = result.Where(x => x.Status == request.FilterParams.CommentStatus);

        if (@param.StartDate != null)
            result = result.Where(x => x.CreationDate.Date >= @param.StartDate.Value.Date);

        if (@param.EndDate != null)
            result = result.Where(x => x.CreationDate.Date <= @param.EndDate.Value.Date);

        var skip = (@param.PageId - 1) * @param.Take;
        var model = new CommentFilterResult()
        {
            Data = await result.Skip(skip).Take(@param.Take)
            .Select(x => x.MapFilterComment())
            .ToListAsync(),
            FilterParams = @param
        };
        model.GeneratePaging(result, @param.Take, @param.PageId);
        return model;
    }
}
