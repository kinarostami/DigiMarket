using Common.Domain;
using Common.Domain.Exceptions;
using Shop.Domain.SellerAgg.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.SellerAgg;

public class Seller : AggregateRoot
{
    public long UserId { get; set; }
    public string ShopName { get; set; }
    public string NationalCode { get; set; }
    public SellerStatus Status { get; set; }
    public DateTime LastUpdate { get; set; }
    public List<SellerInventory> Inventories { get; set; }
    public Seller()
    {
        
    }

    public Seller(long userId, string shopName, string nationalCode, ISellerDomainService domainService)
    {
        Guard(shopName, nationalCode);
        UserId = userId;
        ShopName = shopName;
        NationalCode = nationalCode;
        Inventories = new List<SellerInventory>();

        if (domainService.CheckSellerInfo(this) == false)
            throw new InvalidDomainDataException("اطلاعات نامعتبر است");
    }

    public void Edit(string shopName, string nationCode, ISellerDomainService domainService, SellerStatus status)
    {
        Guard(shopName, nationCode);
        if (nationCode != NationalCode)
            if (domainService.IsNationalCodeExist(nationCode))
                throw new InvalidDomainDataException("کدملی متعلق به شخص دیگری میباشد");
        ShopName = shopName;
        NationalCode = nationCode;
        Status = status;

    }

    public void ChangeStatus(SellerStatus status)
    {
        Status = status;
        LastUpdate = DateTime.Now;
    }

    public void AddInventory(SellerInventory inventory)
    {
        if (Inventories.Any(x => x.ProductId == inventory.ProductId))
            throw new InvalidDomainDataException("این محصول قبلا ثبت شده");

        Inventories.Add(inventory);
    }

    public void EditInventory(long inventoryId, int count, int price, int? discountPercentage)
    {
        var currentInventory = Inventories.FirstOrDefault(x => x.Id ==  inventoryId);
        if (currentInventory == null)
            throw new NullOrEmptyDomainDataException("محصول یافت نشد");

        currentInventory.Edit(count, price, discountPercentage);
    }

    public void DeleteInventory(long inventoryId)
    {
        var currentInventory = Inventories.FirstOrDefault(x => x.Id == inventoryId);
        if (currentInventory == null)
            throw new NullOrEmptyDomainDataException("محصول یافت نشد");

        Inventories.Remove(currentInventory);

    }

    public void Guard(string shopName, string nationCode)
    {
        NullOrEmptyDomainDataException.CheckString(shopName, nameof(shopName));
        NullOrEmptyDomainDataException.CheckString(nationCode, nameof(nationCode));
        if (IranianNationalIdChecker.IsValid(nationCode) == false)
            throw new InvalidDomainDataException("کد ملی نامعتبر است");
    }

}
