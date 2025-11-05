using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastucture.Persistent.Ef;
using Shop.Query.Sellers.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Query.Sellers.GetByUserId;

public record GetSellerByUserIdQuery(long UserId) : IQuery<SellerDto>
{
}

public class GetSellerByUserIdQueryHandler : IQueryHandler<GetSellerByUserIdQuery, SellerDto>
{
    private readonly ShopContext _context;

    public GetSellerByUserIdQueryHandler(ShopContext context)
    {
        _context = context;
    }

    public async Task<SellerDto> Handle(GetSellerByUserIdQuery request, CancellationToken cancellationToken)
    {
        var seller = await _context.Sellers.FirstOrDefaultAsync(x => x.UserId == request.UserId);
        return seller.Map();
    }
}
