using Common.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.SiteEntities.ShippingMethod.Create;
using Shop.Application.SiteEntities.ShippingMethod.Edit;
using Shop.Presentation.Facade.Siteentities.ShippingMethods;
using Shop.Query.SiteEntites.DTOs;

namespace Shop.Api.Controllers;


//[Authorize]
public class ShippingMethodController : ApiController
{
    private readonly IShippingMethodFacade _facade;

    public ShippingMethodController(IShippingMethodFacade facade)
    {
        _facade = facade;
    }

    //[AllowAnonymous]
    [HttpGet]
    public async Task<ApiResult<List<ShippingMethodDto>>> GetList()
    {
        var result = await _facade.GetList();
        return QueryResult(result);
    }
    [HttpGet("{id}")]
    public async Task<ApiResult<ShippingMethodDto>> GetById(long id)
    {
        var result = await _facade.GetShippingMethodById(id);
        return QueryResult(result);
    }


    [HttpPost]
    public async Task<ApiResult> Create(CreateShippingMethodCommand command)
    {
        var result = await _facade.Create(command);
        return CommandResult(result);
    }

    [HttpPut]
    public async Task<ApiResult> Edit(EditShippingMethodCommand command)
    {
        var result = await _facade.Edit(command);
        return CommandResult(result);
    }

    [HttpDelete("{id}")]
    public async Task<ApiResult> Delete(long id)
    {
        var result = await _facade.Delete(id);
        return CommandResult(result);
    }
}