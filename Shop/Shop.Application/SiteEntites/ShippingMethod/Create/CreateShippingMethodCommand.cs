using Common.Application;

namespace Shop.Application.SiteEntities.ShippingMethod.Create;

public record CreateShippingMethodCommand(int Cost,string Title) : IBaseCommand;