using Common.Application;
using Shop.Domain.OrderAgg;
using Shop.Domain.OrderAgg.Repository;
using Shop.Domain.OrderAgg.ValueObject;
using Shop.Domain.SiteEntities.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Orders.CheckOut;

public class CheckOutOrderCommand : IBaseCommand
{
    public CheckOutOrderCommand(long userId, string shire, string city, string postalCode, string postalAddress, string phoneNumber, string name, string family, string nationalCode, long shippingMethodId)
    {
        UserId = userId;
        Shire = shire;
        City = city;
        PostalCode = postalCode;
        PostalAddress = postalAddress;
        PhoneNumber = phoneNumber;
        Name = name;
        Family = family;
        NationalCode = nationalCode;
        ShippingMethodId = shippingMethodId;
    }
    public long UserId { get; set; }
    public string Shire { get; set; }
    public string City { get; set; }
    public string PostalCode { get; set; }
    public string PostalAddress { get; set; }
    public string PhoneNumber { get; set; }
    public string Name { get; set; }
    public string Family { get; set; }
    public string NationalCode { get; set; }
    public long ShippingMethodId { get; private set; }
}
public class CheckOutOrderCommandHandler : IBaseCommandHandler<CheckOutOrderCommand>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IShippingMethodRepository _shippingMethodRepository;

    public CheckOutOrderCommandHandler(IOrderRepository orderRepository, IShippingMethodRepository shippingMethodRepository)
    {
        _orderRepository = orderRepository;
        _shippingMethodRepository = shippingMethodRepository;
    }

    public async Task<OperationResult> Handle(CheckOutOrderCommand request, CancellationToken cancellationToken)
    {
        var currentOrder = await _orderRepository.GetCurrentUserOrder(request.UserId);
        if (currentOrder == null)
            return OperationResult.NotFound();

        var shippingMethod = await _shippingMethodRepository.GetAsync(request.ShippingMethodId);
        if (shippingMethod == null)
            return OperationResult.Error();

        var address = new OrderAddress(request.Shire, request.City, request.PostalCode, request.PostalAddress,
            request.PhoneNumber, request.Name, request.Family, request.NationalCode);
        currentOrder.CheckOut(address, new OrderShippingMethod(shippingMethod.Title, shippingMethod.Cost));

        await _orderRepository.Save();
        return OperationResult.Success();
    }
}
