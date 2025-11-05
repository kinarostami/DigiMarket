using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastucture.Persistent.Ef;
using Shop.Query.Orders.DTOs;
using Shop.Query.Roles.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Query.Roles.GetById;

public record GetRoleByIdQuery(long RoleId) : IQuery<RoleDto>
{
}

public class GetRoleByIdQueryHandler : IQueryHandler<GetRoleByIdQuery, RoleDto>
{
    private readonly ShopContext _context;

    public GetRoleByIdQueryHandler(ShopContext context)
    {
        _context = context;
    }

    public async Task<RoleDto> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
    {
        var role = await _context.Roles.FirstOrDefaultAsync(x => x.Id == request.RoleId);
        if (role == null)
            return null;
        
        return new RoleDto()
        {
            Id = role.Id,
            CreationDate = role.CreationDate,
            Title = role.Title,
            Permissions = role.Permissions.Select(x => x.Permission).ToList()
        };
    }
}
