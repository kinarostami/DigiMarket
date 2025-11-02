using Common.Application;
using Shop.Domain.OrderAgg;
using Shop.Domain.OrderAgg.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Orders.Finally;

public record FinallyOrderCommand(long OrderId) : IBaseCommand
{
}
public class FinallyOrderCommandHandler : IBaseCommandHandler<FinallyOrderCommand>
{
    private readonly IOrderRepository _orderRepository;

    public FinallyOrderCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<OperationResult> Handle(FinallyOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetTracking(request.OrderId);
        if (order == null)
            return OperationResult.NotFound();

        order.Finally();
        await _orderRepository.Save();
        return OperationResult.Success();
    }
}
