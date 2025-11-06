using Common.Application;
using MediatR;
using Shop.Application.Sellers.Create;
using Shop.Application.Sellers.Edit;
using Shop.Query.Sellers.DTOs;
using Shop.Query.Sellers.GetByFilter;
using Shop.Query.Sellers.GetById;
using Shop.Query.Sellers.GetByUserId;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Shop.Presentation.Facade.Sellers;

public interface ISellerFacade
{
    Task<OperationResult> Create(CreateSellerCommand command);
    Task<OperationResult> Edit(EditSellerCommand command);

    Task<SellerFilterResult> GetSellerByFilter(SellerFilterParams filterParams);
    Task<SellerDto?> GetSellerById(long sellerId);
    Task<SellerDto?> GetSellerByUserId(long userId);
}
public class SellerFacade : ISellerFacade
{
    private readonly IMediator _mediator;

    public SellerFacade(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<OperationResult> Create(CreateSellerCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> Edit(EditSellerCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<SellerFilterResult> GetSellerByFilter(SellerFilterParams filterParams)
    {
        return await _mediator.Send(new GetSellerByFilterQuery(filterParams));
    }

    public async Task<SellerDto?> GetSellerById(long sellerId)
    {
        return await _mediator.Send(new GetSellerByIdQuery(sellerId));
    }

    public async Task<SellerDto?> GetSellerByUserId(long userId)
    {
        return await _mediator.Send(new GetSellerByUserIdQuery(userId));
    }
}