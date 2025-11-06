using Common.Application;
using Shop.Application.SiteEntites.Banner.Create;
using Shop.Application.SiteEntities.Banner.Edit;
using Shop.Query.SiteEntites.DTOs;

namespace Shop.Presentation.Facade.SiteEntities.Banner;

public interface IBannerFacade
{
    Task<OperationResult> CreateBanner(CreateBannerCommand command);
    Task<OperationResult> EditBanner(EditBannerCommand command);
    Task<OperationResult> DeleteBanner(long bannerId);

    Task<BannerDto?> GetBannerById(long id);
    Task<List<BannerDto>> GetBanners();
}