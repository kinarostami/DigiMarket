using Common.Query;
using Shop.Query.SiteEntites.DTOs;

namespace Shop.Query.SiteEntities.Sliders.GetById;

public record GetSliderByIdQuery(long SliderId) : IQuery<SliderDto>;

