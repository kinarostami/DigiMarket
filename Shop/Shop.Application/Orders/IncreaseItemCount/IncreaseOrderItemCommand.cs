using Common.Application;
using FluentValidation;
using Shop.Domain.OrderAgg.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Orders.IncreaseItemCount;

public record IncreaseOrderItemCommand(long UserId,long ItemId,int Count) : IBaseCommand
{

}
public class IncreaseOrderItemCommandHandler : IBaseCommandHandler<IncreaseOrderItemCommand>
{
    private readonly IOrderRepository _orderRepository;

    public IncreaseOrderItemCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<OperationResult> Handle(IncreaseOrderItemCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetCurrentUserOrder(request.UserId);
        if (order == null)
            return OperationResult.NotFound();

        order.IncreaseItemCount(request.ItemId, request.Count);
        await _orderRepository.Save();
        return OperationResult.Success();
    }
}
public class IncreaseOrderItemCommandValidator : AbstractValidator<IncreaseOrderItemCommand>
{
    public IncreaseOrderItemCommandValidator()
    {
        RuleFor(x => x.Count).GreaterThanOrEqualTo(1).WithMessage("تعداد باید بیشتر از صفر باشد");
    }
}
