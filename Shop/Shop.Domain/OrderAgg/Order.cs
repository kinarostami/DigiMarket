using Common.Domain;
using Common.Domain.Exceptions;
using Shop.Domain.OrderAgg.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.OrderAgg;

public class Order : AggregateRoot
{
    public Order()
    {
        
    }
    public long UserId { get; set; }
    public OrderStatus Status { get; set; }
    public OrderDiscount? Discount { get; set; }
    public OrderAddress? Address { get; set; }
    public OrderShippingMethod ShippingMethod { get; set; }
    public List<OrderItem> Items { get; set; }
    public DateTime LastUpdate { get; set; }
    public int TotalPrice
    {
        get
        {
            var totalPrice = Items.Sum(x => x.TotalPrice);
            if (ShippingMethod != null)
                totalPrice += ShippingMethod.ShippingCost;

            if (Discount != null)
                totalPrice -= Discount.DiscountAmount;

            return totalPrice;
        }
    }
    public int ItemCount => Items.Count;

    public void AddItem(OrderItem item)
    {
        ChangeOrderGuard();

        var oldItem = Items.FirstOrDefault(x => x.InventoryId == item.Id);
        if (oldItem != null)
        {
            oldItem.ChangeCount(item.Count += oldItem.Count);
        }
        Items.Add(item);
    }

    public void RemoveItem(long itemId)
    {
        ChangeOrderGuard();

        var currentItem = Items.FirstOrDefault(x => x.Id == itemId);
        if (currentItem != null)
            Items.Remove(currentItem);
    }

    public void IncreaseItemCount(long itemId, int count)
    {
        ChangeOrderGuard();

        var currentItem = Items.FirstOrDefault(x => x.Id == itemId);
        if (currentItem == null)
            throw new NullOrEmptyDomainDataException();

        currentItem.IncreaseCount(count);
    }

    public void DecreaseItemCount(long itemId, int count)
    {
        ChangeOrderGuard();

        var currentItem = Items.FirstOrDefault(x => x.Id == itemId);
        if (currentItem == null)
            throw new NullOrEmptyDomainDataException();

        currentItem.DecreaseCount(count);
    }

    public void ChangeCountItem(long itemId, int newCount)
    {
        ChangeOrderGuard();

        var currentItem = Items.FirstOrDefault(x => x.Id == itemId);
        if (currentItem == null)
            throw new NullOrEmptyDomainDataException();

        currentItem.ChangeCount(newCount);
    }

    public void Finally()
    {
        Status = OrderStatus.Finally;
        LastUpdate = DateTime.Now;
        AddDomainEvent(new OrderFinalized(Id));
    }

    public void ChangeStatus(OrderStatus status)
    {
        Status = status;
        LastUpdate = DateTime.Now;
    }

    public void CheckOut(OrderAddress orderAddress, OrderShippingMethod shippingMethod)
    {
        ChangeOrderGuard();

        Address = orderAddress;
        ShippingMethod = shippingMethod;
    }

    public void ChangeOrderGuard()
    {
        if (Status != OrderStatus.Pending)
            throw new InvalidDomainDataException("امکان ویرایش این سفارش وجود ندارد");
    }
}
