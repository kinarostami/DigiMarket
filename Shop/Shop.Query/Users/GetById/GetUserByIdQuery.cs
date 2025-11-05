using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastucture.Persistent.Ef;
using Shop.Query.Users.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Query.Users.GetById;

public record GetUserByIdQuery(long UserId) : IQuery<UserDto>
{
}

public class GetUserByIdQueryHandler : IQueryHandler<GetUserByIdQuery, UserDto>
{
    private readonly ShopContext _context;

    public GetUserByIdQueryHandler(ShopContext context)
    {
        _context = context;
    }

    public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var model = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.UserId);
        if (model == null)
            return null;
        return await model.Map().SetUserRoleTitles(_context);
    }
}
