using Common.Domain;
using Common.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.ProductAgg;

public class ProductSpecification : BaseEntity
{
    public ProductSpecification(string key, string value)
    {
        NullOrEmptyDomainDataException.CheckString(key, nameof(key));
        NullOrEmptyDomainDataException.CheckString(value, nameof(value));

        Key = key;
        Value = value;
    }
    public long ProductId { get; set; }
    public string Key { get; set; }
    public string Value { get; set; }
}
