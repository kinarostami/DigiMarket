using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastucture.Persistent.Ef;
using Shop.Query.SiteEntites.DTOs;

namespace Shop.Query.SiteEntities.Sliders.GetById;

internal class GetSliderByIdQueryHandler : IQueryHandler<GetSliderByIdQuery, SliderDto>
{
    private readonly ShopContext _context;
    public GetSliderByIdQueryHandler(ShopContext context)
    {
        _context = context;
    }

    public async Task<SliderDto> Handle(GetSliderByIdQuery request, CancellationToken cancellationToken)
    {
        var slider = await _context.Sliders.FirstOrDefaultAsync(x => x.Id == request.SliderId,cancellationToken);
        if (slider == null)
            return null;

        return new SliderDto()
        {
            Id = slider.Id,
            Title = slider.Title,
            CreationDate = slider.CreationDate,
            ImageName = slider.ImageName,
            Link = slider.Link
        };
    }
}

