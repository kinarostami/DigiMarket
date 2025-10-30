using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.OrderAgg.ValueObject;

public class OrderDiscount : Common.Domain.ValueObject
{
    public string DiscountTitle { get; set; }
    public int DiscountAmount { get; set; }

    public OrderDiscount(string discountTitle, int discountAmount)
    {
        DiscountTitle = discountTitle;
        DiscountAmount = discountAmount;
    }
}
