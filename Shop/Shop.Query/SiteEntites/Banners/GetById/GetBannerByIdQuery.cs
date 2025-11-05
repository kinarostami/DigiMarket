using Common.Query;
using Shop.Query.SiteEntites.DTOs;

namespace Shop.Query.SiteEntities.Banners.GetById;

public record GetBannerByIdQuery(long BannerId) : IQuery<BannerDto>;

