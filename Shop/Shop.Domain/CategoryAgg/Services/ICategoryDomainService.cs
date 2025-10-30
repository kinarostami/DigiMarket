using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.CategoryAgg.Services
{
    public interface ICategoryDomainService
    {
        public bool IsSlugExist(string slug);
    }
}
