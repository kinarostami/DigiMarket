using Common.Application;
using Common.ChachHelper;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Shop.Application.Products.AddImage;
using Shop.Application.Products.Create;
using Shop.Application.Products.Edit;
using Shop.Application.Products.RemoveImage;
using Shop.Infrastucture.Persistent.Ef;
using Shop.Presentation.Facade.Sellers.Inventories;
using Shop.Query.Orders.DTOs;
using Shop.Query.Products.DTOs;
using Shop.Query.Products.GetByFilter;
using Shop.Query.Products.GetById;
using Shop.Query.Products.GetBySlug;
using Shop.Query.Products.GetForShop;
using Shop.Query.Sellers.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Shop.Presentation.Facade.Products;

public interface IProductFacade
{
    Task<OperationResult> Create(CreateProductCommand command);
    Task<OperationResult> AddImage(AddImageCommand command);
    Task<OperationResult> Edit(EditProductCommand command);
    Task<OperationResult> RemoveImage(RemoveImageCommand command);

    Task<ProductFilterResult> GetProductByFilter(ProductFilterParams filterParams);
    Task<ProductDto> GetProductById(long productId);
    Task<ProductDto> GetProductBySlug(string slug);
    Task<SingleProductDto?> GetProductBySlugForSinglePage(string slug);
    Task<ProductShopResult> GetProductsForShop(ProductShopFilterParam filterParams);
}

public class SingleProductDto
{
    public ProductDto ProductDto { get; set; }
    public List<InventoryDto> Inventories { get; set; }
}

public class ProductFacade : IProductFacade
{
    private readonly IMediator _mediator;
    private readonly IDistributedCache _cache;
    private readonly ISellerInventoryFacade _inventoryFacade;
    public ProductFacade(IMediator mediator, IDistributedCache cache, ISellerInventoryFacade inventoryFacade)
    {
        _mediator = mediator;
        _cache = cache;
        _inventoryFacade = inventoryFacade;
    }

    public async Task<OperationResult> AddImage(AddImageCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> Create(CreateProductCommand command)
    {
        return  await _mediator.Send(command);
    }

    public async Task<OperationResult> Edit(EditProductCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<ProductFilterResult> GetProductByFilter(ProductFilterParams filterParams)
    {
        return await _mediator.Send(new GetProductByFilterQuery(filterParams));
    }

    public async Task<ProductDto> GetProductById(long productId)
    {
        return await _mediator.Send(new GetProductByIdQuery(productId));
    }

    public async Task<ProductDto> GetProductBySlug(string slug)
    {
        return await _mediator.Send(new GetProductBySlugQuery(slug));
    }

    public async Task<SingleProductDto?> GetProductBySlugForSinglePage(string slug)
    {
        return await _cache.GetOrSet(CacheKeys.Product(slug), async () =>
        {
            var product = await _mediator.Send(new GetProductBySlugQuery(slug));
            if (product == null)
                return null;

            var inventories = await _inventoryFacade.GetByProductId(product.Id);
            var model = new SingleProductDto()
            {
                Inventories = inventories,
                ProductDto = product
            };
            return model;
        });
    }

    public async Task<ProductShopResult> GetProductsForShop(ProductShopFilterParam filterParams)
    {
        return await _mediator.Send(new GetProductsForShopQuery(filterParams));
    }

    public async Task<OperationResult> RemoveImage(RemoveImageCommand command)
    {
        return await _mediator.Send(command);
    }
}
