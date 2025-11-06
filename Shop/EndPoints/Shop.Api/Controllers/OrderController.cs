using Common.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Orders.AddItem;
using Shop.Application.Orders.CheckOut;
using Shop.Application.Orders.DecreaseItemCount;
using Shop.Application.Orders.IncreaseItemCount;
using Shop.Application.Orders.RemoveItem;
using Shop.Application.Orders.SendOrder;
using Shop.Domain.OrderAgg;
using Shop.Presentation.Facade.Orders;
using Shop.Query.Orders.DTOs;

namespace Shop.Api.Controllers;

public class OrderController : ApiController
{
    private readonly IOrderFacade _orderFacade;

    public OrderController(IOrderFacade orderFacade)
    {
        _orderFacade = orderFacade;
    }
    [HttpGet]
    public async Task<ApiResult<OrderFilterResult>> GetOrderByFilter([FromQuery] OrderFilterParam filterParam)
    {
        var result = await _orderFacade.GetOrderByFilter(filterParam);
        return QueryResult(result);
    }

    [HttpGet("current/filter")]
    public async Task<ApiResult<OrderFilterResult>> GetUsersOrderByFilter(int pageId = 1, int take = 10, OrderStatus status = OrderStatus.Finally)
    {
        var result = await _orderFacade.GetOrderByFilter(new OrderFilterParam()
        {
            PageId = pageId,
            Take = take,
            Status = status,
            EndDate = null,
            StartDate = null,
            UserId = User.GetUserId()
        });
        return QueryResult(result);
    }

    [HttpGet("current")]
    public async Task<ApiResult<OrderDto?>> GetCurrentOrder()
    {
        var result = await _orderFacade.GetCurrentUserId(User.GetUserId());
        return QueryResult(result);
    }

    [HttpGet("{orderId}")]
    public async Task<ApiResult<OrderDto?>> GetOrderById(long orderId)
    {
        var result = await _orderFacade.GetOrderById(orderId);
        return QueryResult(result);
    }

    [HttpPost]
    public async Task<ApiResult> AddOrderItem(AddOrderItemCommand command)
    {
        var result = await _orderFacade.AddItem(command);
        return CommandResult(result);
    }

    [HttpPost("Checkout")]
    public async Task<ApiResult> AddOrderItem(CheckOutOrderCommand command)
    {
        var result = await _orderFacade.CheckOut(command);
        return CommandResult(result);
    }

    [HttpPut("SendOrder/{orderId}")]
    public async Task<ApiResult> SendOrder(SendOrderCommand command)
    {
        var result = await _orderFacade.SendOrder(command);
        return CommandResult(result);
    }

    [HttpPut("orderItem/IncreaseCount")]
    public async Task<ApiResult> IncreaseOrderItemCount(IncreaseOrderItemCommand command)
    {
        var result = await _orderFacade.IncreaseItemCount(command);
        return CommandResult(result);
    }

    [HttpPut("orderItem/DecreaseCount")]
    public async Task<ApiResult> DecreaseOrderItemCount(DecreaseOrderItemCommand command)
    {
        var result = await _orderFacade.DecreaseItemCount(command);
        return CommandResult(result);
    }

    [HttpDelete("orderItem")]
    public async Task<ApiResult> RemoveOrderItem(RemoveOrderItemCommand command)
    {
        var result = await _orderFacade.RemoveItem(command);
        return CommandResult(result);
    }
}
