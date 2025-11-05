using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastucture.Persistent.Ef;
using Shop.Query.SiteEntites.DTOs;

namespace Shop.Query.SiteEntities.Sliders.GetList;

internal class GetSliderListQueryHandler : IQueryHandler<GetSliderListQuery, List<SliderDto>>
{
    private readonly ShopContext _context;

    public GetSliderListQueryHandler(ShopContext context)
    {
        _context = context;
    }

    public async Task<List<SliderDto>> Handle(GetSliderListQuery request, CancellationToken cancellationToken)
    {
        return await _context.Sliders
            .OrderByDescending(x => x.Id)
            .Select(x => new SliderDto()
            {
                Id = x.Id,
                Title = x.Title,
                CreationDate = x.CreationDate,
                ImageName = x.ImageName,
                Link = x.Link
            }).ToListAsync();
    }
}
