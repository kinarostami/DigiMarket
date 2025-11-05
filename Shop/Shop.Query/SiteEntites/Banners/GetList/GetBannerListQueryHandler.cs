using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastucture.Persistent.Ef;
using Shop.Query.SiteEntites.DTOs;

namespace Shop.Query.SiteEntities.Banners.GetList;

public class GetBannerListQueryHandler : IQueryHandler<GetBannerListQuery, List<BannerDto>>
{
    private readonly ShopContext _context;

    public GetBannerListQueryHandler(ShopContext context)
    {
        _context = context;
    }

    public Task<List<BannerDto>> Handle(GetBannerListQuery request, CancellationToken cancellationToken)
    {
        return _context.Banners
            .OrderByDescending(x => x.Id)
            .Select(x => new BannerDto()
        {
            Id = x.Id,
            CreationDate = x.CreationDate,
            Position = x.Position,
            ImageName = x.ImageName,
            Link = x.Link
        }).ToListAsync(cancellationToken);
    }
}
