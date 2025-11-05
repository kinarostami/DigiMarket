using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastucture.Persistent.Dapper;
using Shop.Infrastucture.Persistent.Ef;
using Shop.Query.Orders.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Query.Orders.GetCurrent;

public record GetCurrentUserOrderQuery(long UserId) : IQuery<OrderDto?>
{
}
public class GetCurrentUserOrderQueryHandler : IQueryHandler<GetCurrentUserOrderQuery, OrderDto?>
{
    private readonly ShopContext _context;
    private readonly DapperContext _dapperContext;

    public GetCurrentUserOrderQueryHandler(ShopContext context, DapperContext dapperContext)
    {
        _context = context;
        _dapperContext = dapperContext;
    }

    public async Task<OrderDto?> Handle(GetCurrentUserOrderQuery request, CancellationToken cancellationToken)
    {
        var order = await _context.Orders
            .FirstOrDefaultAsync(x => x.UserId == request.UserId || x.Status == Domain.OrderAgg.OrderStatus.Pending);
        if (order == null)
            return null;

        var orderDto = order.Map();
        orderDto.UserFullName = await _context.Users.Where(f => f.Id == orderDto.UserId)
            .Select(s => $"{s.Name} {s.Family}").FirstAsync(cancellationToken);

        orderDto.Items = await orderDto.GetOrderItems(_dapperContext);
        return orderDto;
    }
}
