using Common.Domain;
using Common.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.ProductAgg;

public class ProductImage : BaseEntity
{
    public ProductImage(string imageName, int sequence)
    {
        NullOrEmptyDomainDataException.CheckString(imageName, nameof(imageName));

        ImageName = imageName;
        Sequence = sequence;
    }
    public long ProductId { get; set; }
    public string ImageName { get; set; }
    public int Sequence { get; set; }
}
