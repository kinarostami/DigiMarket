using Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.OrderAgg;

public class OrderFinalized : BaseDomainEvent
{
    public OrderFinalized(long orderId)
    {
        OrderId = orderId;
    }

    public long OrderId { get; private set; }
}
