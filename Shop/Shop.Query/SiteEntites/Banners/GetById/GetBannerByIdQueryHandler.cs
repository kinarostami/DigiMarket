using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastucture.Persistent.Ef;
using Shop.Query.SiteEntites.DTOs;

namespace Shop.Query.SiteEntities.Banners.GetById;

public class GetBannerByIdQueryHandler : IQueryHandler<GetBannerByIdQuery, BannerDto>
{
    private readonly ShopContext _context;

    public GetBannerByIdQueryHandler(ShopContext context)
    {
        _context = context;
    }

    public async Task<BannerDto> Handle(GetBannerByIdQuery request, CancellationToken cancellationToken)
    {
        var banner = await _context.Banners.FirstOrDefaultAsync(x => x.Id == request.BannerId,cancellationToken);
        if (banner == null)
            return null;
        
        return new BannerDto()
            {
                Id = banner.Id,
                ImageName = banner.ImageName,
                CreationDate = banner.CreationDate,
                Link = banner.Link,
                Position = banner.Position,
            };
    }
}

