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

namespace Shop.Query.Orders.GetById;

public record GetOrderByIdQuery(long OrderId) : IQuery<OrderDto?>
{
}

public class GetOrderByIdQueryHandler : IQueryHandler<GetOrderByIdQuery, OrderDto?>
{
    private readonly ShopContext _context;
    private readonly DapperContext _dapper;

    public GetOrderByIdQueryHandler(ShopContext context, DapperContext dapper)
    {
        _context = context;
        _dapper = dapper;
    }

    public async Task<OrderDto?> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == request.OrderId);
        if (order == null)
            return null;

        var orderDto = order.Map();
        orderDto.UserFullName = await _context.Users.Where(x => x.Id == request.OrderId)
            .Select(x => $"{x.Name}{x.Family}")
            .FirstAsync();

        orderDto.Items = await orderDto.GetOrderItems(_dapper);
        return orderDto;
    }
}
