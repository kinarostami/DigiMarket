using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Infrastucture.Persistent.Ef;
using Shop.Query.Roles.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Query.Roles.GetList;

public class GetRoleListQuery : IQuery<List<RoleDto>>
{
    
}
public class GetRoleListQueryHandler : IQueryHandler<GetRoleListQuery, List<RoleDto>>
{
    private readonly ShopContext _context;

    public GetRoleListQueryHandler(ShopContext context)
    {
        _context = context;
    }

    public async Task<List<RoleDto>> Handle(GetRoleListQuery request, CancellationToken cancellationToken)
    {
        return await _context.Roles.Select(x => new RoleDto()
        {
            Id = x.Id,
            CreationDate = x.CreationDate,
            Title = x.Title,
            Permissions = x.Permissions.Select(x => x.Permission).ToList()
        }).ToListAsync(cancellationToken);
    }
}
