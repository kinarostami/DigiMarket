using Microsoft.EntityFrameworkCore;
using Shop.Domain.OrderAgg;
using Shop.Domain.OrderAgg.Repository;
using Shop.Infrastucture._Utilties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Infrastucture.Persistent.Ef.OrderAgg;

public class OrderRepository : BaseRepository<Order>, IOrderRepository
{
    public OrderRepository(ShopContext context) : base(context)
    {
    }

    public async Task<Order> GetCurrentUserOrder(long userId)
    {
        return await Context.Orders.AsNoTracking()
            .FirstOrDefaultAsync(x => x.UserId == userId && x.Status == OrderStatus.Pending);
    }
}
