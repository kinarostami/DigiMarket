using Common.Domain.Repository;
using Shop.Domain.SiteEntities;
using Shop.Domain.SiteEntities.Repository;
using Shop.Infrastucture._Utilties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Infrastucture.Persistent.Ef.SiteEntitiesAgg.Repositories;

public class ShippingMethodRepository : BaseRepository<ShippingMethod>, IShippingMethodRepository
{
    public ShippingMethodRepository(ShopContext context) : base(context)
    {
    }

    public void Delete(ShippingMethod shipping)
    {
        Context.ShippingMethods.Remove(shipping);
    }
}
