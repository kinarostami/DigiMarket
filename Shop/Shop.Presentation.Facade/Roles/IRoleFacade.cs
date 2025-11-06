using Common.Application;
using MediatR;
using Shop.Application.Roles.Create;
using Shop.Application.Roles.Edit;
using Shop.Query.Orders.DTOs;
using Shop.Query.Roles.DTOs;
using Shop.Query.Roles.GetById;
using Shop.Query.Roles.GetList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Shop.Presentation.Facade.Roles;

public interface IRoleFacade
{
    Task<OperationResult> Create(CreateRoleCommand command);
    Task<OperationResult> Edit(EditRoleCommand command);

    Task<RoleDto> GetRoleById(long roleId);
    Task<List<RoleDto>> GetRoles();
}

public class RoleFacade : IRoleFacade
{
    private readonly IMediator _mediator;

    public RoleFacade(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<OperationResult> Create(CreateRoleCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> Edit(EditRoleCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<RoleDto> GetRoleById(long roleId)
    {
        return await _mediator.Send(new GetRoleByIdQuery(roleId));
    }

    public async Task<List<RoleDto>> GetRoles()
    {
        return await _mediator.Send(new GetRoleListQuery());
    }
}