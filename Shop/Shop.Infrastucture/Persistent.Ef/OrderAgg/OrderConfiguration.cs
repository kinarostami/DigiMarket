using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.OrderAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Infrastucture.Persistent.Ef.OrderAgg;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders", "order");

        builder.OwnsOne(x => x.Discount, option =>
        {
            option.Property(x => x.DiscountTitle).HasMaxLength(50);
        });
        builder.OwnsOne(x => x.ShippingMethod, option =>
        {
            option.Property(x => x.ShippingType).HasMaxLength(50);
        });

        builder.OwnsMany(x => x.Items, option =>
        {
            option.ToTable("Items", "order");
        });
        builder.OwnsOne(x => x.Address, option =>
        {
            option.ToTable("Addresses", "order");
            option.HasKey(x => x.Id);

            option.Property(x => x.City).HasMaxLength(50).IsRequired();

            option.Property(x => x.PhoneNumber).HasMaxLength(11).IsRequired();

            option.Property(x => x.Name).HasMaxLength(100).IsRequired();

            option.Property(x => x.Family).HasMaxLength(100).IsRequired();

            option.Property(x => x.NationalCode).HasMaxLength(11).IsRequired();

            option.Property(x => x.PostalCode).HasMaxLength(40).IsRequired();

            option.Property(x => x.PostalAddress).HasMaxLength(40).IsRequired();
        });
    }
}
