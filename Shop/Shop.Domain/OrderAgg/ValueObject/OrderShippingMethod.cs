using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.OrderAgg.ValueObject;

public class OrderShippingMethod
{
    public OrderShippingMethod(string shippingType, int shippingCost)
    {
        ShippingType = shippingType;
        ShippingCost = shippingCost;
    }

    public string ShippingType { get; set; }
    public int ShippingCost { get; set; }
}
