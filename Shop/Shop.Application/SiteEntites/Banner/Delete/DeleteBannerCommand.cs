using Common.Application;

namespace Shop.Application.SiteEntities.Banner.Delete;

public record DeleteBannerCommand(long Id) : IBaseCommand;