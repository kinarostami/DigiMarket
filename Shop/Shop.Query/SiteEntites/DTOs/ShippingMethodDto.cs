using Common.Query;

namespace Shop.Query.SiteEntites.DTOs;

public class ShippingMethodDto : BaseDto
{
    public string Title { get; set; }
    public int Cost { get; set; }
}