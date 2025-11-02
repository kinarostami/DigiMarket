using Common.Application;
using Shop.Domain.OrderAgg;
using Shop.Domain.OrderAgg.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Orders.SendOrder;

public class SendOrderCommand : IBaseCommand
{
    public SendOrderCommand(long orderId)
    {
        OrderId = orderId;
    }

    public long OrderId { get; set; }
}
public class SendOrderCommandHandler : IBaseCommandHandler<SendOrderCommand>
{
    readonly IOrderRepository _orderRepository;

    public SendOrderCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<OperationResult> Handle(SendOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetTracking(request.OrderId);
        if (order == null)
            return OperationResult.NotFound();

        order.ChangeStatus(OrderStatus.Shipping);
        await _orderRepository.Save();
        return OperationResult.Success();
    }
}
