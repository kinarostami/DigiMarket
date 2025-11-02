using Common.Application;

namespace Shop.Application.SiteEntities.ShippingMethod.Edit;

public record EditShippingMethodCommand(long Id,int Cost,string Title) : IBaseCommand;