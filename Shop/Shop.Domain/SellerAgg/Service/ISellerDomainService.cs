using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.SellerAgg.Service;

public interface ISellerDomainService
{
    bool CheckSellerInfo(Seller seller);
    bool IsNationalCodeExist(string nationalCode);
}
