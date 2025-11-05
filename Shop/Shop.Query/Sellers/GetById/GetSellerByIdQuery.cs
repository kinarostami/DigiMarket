using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastucture.Persistent.Ef;
using Shop.Query.Sellers.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Query.Sellers.GetById;

public record GetSellerByIdQuery(long SellerId) : IQuery<SellerDto>
{
}
public class GetSellerByIdQueryHandler : IQueryHandler<GetSellerByIdQuery, SellerDto>
{
    private readonly ShopContext _context;

    public GetSellerByIdQueryHandler(ShopContext context)
    {
        _context = context;
    }

    public async Task<SellerDto> Handle(GetSellerByIdQuery request, CancellationToken cancellationToken)
    {
        var seller = await _context.Sellers.FirstOrDefaultAsync(x => x.Id == request.SellerId);
        return seller.Map();
    }
}
