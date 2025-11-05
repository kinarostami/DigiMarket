using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastucture.Persistent.Ef;
using Shop.Query.Comments.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Query.Comments.GetById;

public record GetCommentByIdQuery(long CommentId) : IQuery<CommentDto>
{
}
public class GetCommentByIdQueryHandler : IQueryHandler<GetCommentByIdQuery, CommentDto>
{
    private readonly ShopContext _context;

    public GetCommentByIdQueryHandler(ShopContext context)
    {
        _context = context;
    }

    public async Task<CommentDto> Handle(GetCommentByIdQuery request, CancellationToken cancellationToken)
    {
        var model = await _context.Comments.FirstOrDefaultAsync(x => x.Id == request.CommentId);
        return model.Map();
    }
}
