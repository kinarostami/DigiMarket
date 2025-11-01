using Shop.Domain.SellerAgg;
using Shop.Domain.SellerAgg.Repository;
using Shop.Domain.SellerAgg.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Sellers;

public class SellerDomainService : ISellerDomainService
{
    private readonly ISellerRepository _sellerRepository;

    public SellerDomainService(ISellerRepository sellerRepository)
    {
        _sellerRepository = sellerRepository;
    }

    public bool CheckSellerInfo(Seller seller)
    {
        var sellerInventory = _sellerRepository.Exists(x => x.NationalCode == seller.NationalCode || x.UserId == seller.UserId);
        return !sellerInventory;
    }

    public bool IsNationalCodeExist(string nationalCode)
    {
        return _sellerRepository.Exists(x => x.NationalCode ==  nationalCode);
    }
}
