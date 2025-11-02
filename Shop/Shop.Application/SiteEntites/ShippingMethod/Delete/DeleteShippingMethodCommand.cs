using Common.Application;

namespace Shop.Application.SiteEntities.ShippingMethod.Delete;

public record DeleteShippingMethodCommand(long Id) : IBaseCommand;