using Common.Application;
using MediatR;
using Shop.Application.Sellers.AddInventory;
using Shop.Application.Sellers.EditInventory;
using Shop.Domain.ProductAgg;
using Shop.Query.Sellers.DTOs;
using Shop.Query.Sellers.Inventories.GetById;
using Shop.Query.Sellers.Inventories.GetByProductId;
using Shop.Query.Sellers.Inventories.GetList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Shop.Presentation.Facade.Sellers.Inventories;

public interface ISellerInventoryFacade
{
    Task<OperationResult> AddInventory(AddInventoryCommand command);
    Task<OperationResult> EditInventory(EditInventoryCommand command);

    Task<List<InventoryDto>> GetList(long sellerId,long productId);
    Task<InventoryDto?> GetInventoryById(long inventoryId,long productId,long sellerId);
    Task<List<InventoryDto>> GetInventoryByProductId(long productId,long selleId);
}
public class SellerInventoryFacade : ISellerInventoryFacade
{
    private readonly IMediator _mediator;

    public SellerInventoryFacade(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<OperationResult> AddInventory(AddInventoryCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> EditInventory(EditInventoryCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<InventoryDto?> GetInventoryById(long inventoryId, long productId, long sellerId)
    {
        return await _mediator.Send(new GetInventoryByIdQuery(inventoryId,productId,sellerId));
    }

    public async Task<List<InventoryDto>> GetInventoryByProductId(long productId,long sellerId)
    {
        return await _mediator.Send(new GetInventoryByProductIdQuery(productId,sellerId));
    }

    public async Task<List<InventoryDto>> GetList(long sellerId,long productId)
    {
        return await _mediator.Send(new GetListInventoryQuery(sellerId,productId));
    }
}