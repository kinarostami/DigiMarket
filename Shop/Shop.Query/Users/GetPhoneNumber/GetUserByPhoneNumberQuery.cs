using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastucture.Persistent.Ef;
using Shop.Query.Users.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Query.Users.GetPhoneNumber;

public record GetUserByPhoneNumberQuery(string phoneNumber) : IQuery<UserDto>
{
}

public class GetUserByPhoneNumberQueryHandler : IQueryHandler<GetUserByPhoneNumberQuery, UserDto>
{
    private readonly ShopContext _context;

    public GetUserByPhoneNumberQueryHandler(ShopContext context)
    {
        _context = context;
    }

    public async Task<UserDto> Handle(GetUserByPhoneNumberQuery request, CancellationToken cancellationToken)
    {
        var model = await _context.Users.FirstOrDefaultAsync(x => x.PhoneNumber == request.phoneNumber);
        if (model == null)
            return null;
        return await model.Map().SetUserRoleTitles(_context);
    }
}
