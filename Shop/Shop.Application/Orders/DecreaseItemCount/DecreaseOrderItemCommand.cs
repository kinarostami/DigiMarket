using Common.Application;
using FluentValidation;
using Shop.Domain.OrderAgg.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Orders.DecreaseItemCount;

public record DecreaseOrderItemCommand(long UserId, long ItemId, int Count) : IBaseCommand
{
}
public class DecreaseOrderItemCommandHandler : IBaseCommandHandler<DecreaseOrderItemCommand>
{
    private readonly IOrderRepository _orderRepository;

    public DecreaseOrderItemCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<OperationResult> Handle(DecreaseOrderItemCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetCurrentUserOrder(request.UserId);
        if (order == null)
            return OperationResult.NotFound();

        order.DecreaseItemCount(request.ItemId, request.Count);
        await _orderRepository.Save();
        return OperationResult.Success();
    }
}
public class DecreaseOrderItemCommandValidator : AbstractValidator<DecreaseOrderItemCommand>
{
    public DecreaseOrderItemCommandValidator()
    {
        RuleFor(x => x.Count).GreaterThanOrEqualTo(1).WithMessage("تعداد باید بیشتر از صفر باشد");
    }
}
