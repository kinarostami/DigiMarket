using Shop.Domain.SiteEntities;
using Shop.Domain.SiteEntities.Repository;
using Shop.Infrastucture._Utilties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Infrastucture.Persistent.Ef.SiteEntitiesAgg.Repositories;

public class SliderRepository : BaseRepository<Slider>, ISliderRepository
{
    public SliderRepository(ShopContext context) : base(context)
    {
    }

    public void Delete(Slider slider)
    {
        Context.Sliders.Remove(slider);
    }
}
