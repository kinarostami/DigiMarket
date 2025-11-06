using Common.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.SiteEntites.Banner.Create;
using Shop.Application.SiteEntities.Banner.Edit;
using Shop.Presentation.Facade.SiteEntities.Banner;
using Shop.Query.SiteEntites.DTOs;

namespace Shop.Api.Controllers;

//[PermissionChecker(Permission.CRUD_Banner)]
public class BannerController : ApiController
{
    private readonly IBannerFacade _facade;


    public BannerController(IBannerFacade facade)
    {
        _facade = facade;
    }

    //[AllowAnonymous]
    [HttpGet]
    public async Task<ApiResult<List<BannerDto>>> GetList()
    {
        var result = await _facade.GetBanners();
        return QueryResult(result);
    }

    [HttpGet("{id}")]
    public async Task<ApiResult<BannerDto?>> GetById(long id)
    {
        var result = await _facade.GetBannerById(id);
        return QueryResult(result);
    }

    [HttpPost]
    public async Task<ApiResult> Create([FromForm] CreateBannerCommand command)
    {
        var result = await _facade.CreateBanner(command);
        return CommandResult(result);
    }
    [HttpPut]
    public async Task<ApiResult> Edit([FromForm] EditBannerCommand command)
    {
        var result = await _facade.EditBanner(command);
        return CommandResult(result);
    }

    [HttpDelete("{bannerId}")]
    public async Task<ApiResult> Delete(long bannerId)
    {
        var result = await _facade.DeleteBanner(bannerId);
        return CommandResult(result);
    }
}