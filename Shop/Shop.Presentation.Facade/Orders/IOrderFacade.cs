using Common.Application;
using MediatR;
using Shop.Application.Orders.AddItem;
using Shop.Application.Orders.CheckOut;
using Shop.Application.Orders.DecreaseItemCount;
using Shop.Application.Orders.Finally;
using Shop.Application.Orders.IncreaseItemCount;
using Shop.Application.Orders.RemoveItem;
using Shop.Application.Orders.SendOrder;
using Shop.Query.Orders.DTOs;
using Shop.Query.Orders.GetByFilter;
using Shop.Query.Orders.GetById;
using Shop.Query.Orders.GetCurrent;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Shop.Presentation.Facade.Orders;

public interface IOrderFacade
{
    Task<OperationResult> AddItem(AddOrderItemCommand command);
    Task<OperationResult> CheckOut(CheckOutOrderCommand command);
    Task<OperationResult> DecreaseItemCount(DecreaseOrderItemCommand command);
    Task<OperationResult> Finally(FinallyOrderCommand command);
    Task<OperationResult> IncreaseItemCount(IncreaseOrderItemCommand command);
    Task<OperationResult> RemoveItem(RemoveOrderItemCommand command);
    Task<OperationResult> SendOrder(SendOrderCommand command);

    Task<OrderFilterResult> GetOrderByFilter(OrderFilterParam filterParam);
    Task<OrderDto> GetOrderById(long orderId);
    Task<OrderDto> GetCurrentUserId(long userId);
}
public class OrderFacade : IOrderFacade
{
    private readonly IMediator _mediator;

    public OrderFacade(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<OperationResult> AddItem(AddOrderItemCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> CheckOut(CheckOutOrderCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> DecreaseItemCount(DecreaseOrderItemCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> Finally(FinallyOrderCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OrderDto> GetCurrentUserId(long userId)
    {
        return await _mediator.Send(new GetCurrentUserOrderQuery(userId));
    }

    public async Task<OrderFilterResult> GetOrderByFilter(OrderFilterParam filterParam)
    {
        return await _mediator.Send(new GetOrderByFilterQuery(filterParam));
    }

    public async Task<OrderDto> GetOrderById(long orderId)
    {
        return await _mediator.Send(new GetOrderByIdQuery(orderId));
    }

    public async Task<OperationResult> IncreaseItemCount(IncreaseOrderItemCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> RemoveItem(RemoveOrderItemCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> SendOrder(SendOrderCommand command)
    {
        return await _mediator.Send(command);
    }
}
