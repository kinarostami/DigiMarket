using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.ProductAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Infrastucture.Persistent.Ef.ProductAgg;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products", "product");
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Slug).IsUnique();

        builder.Property(x => x.Title).IsRequired().HasMaxLength(300);
        builder.Property(x => x.Discription).IsRequired();
        builder.Property(x => x.ImageName).IsRequired().HasMaxLength(110);
        builder.Property(x => x.Slug).IsRequired().IsUnicode(false);

        builder.OwnsOne(b => b.SeoData, config =>
        {
            config.Property(b => b.MetaDescription)
                .HasMaxLength(500)
                .HasColumnName("MetaDescription");

            config.Property(b => b.MetaTitle)
                .HasMaxLength(500)
                .HasColumnName("MetaTitle");

            config.Property(b => b.MetaKeyWords)
                .HasMaxLength(500)
                .HasColumnName("MetaKeyWords");

            config.Property(b => b.IndexPage)
                .HasColumnName("IndexPage");

            config.Property(b => b.Canonical)
                .HasMaxLength(500)
                .HasColumnName("Canonical");

            config.Property(b => b.Schema)
                .HasColumnName("Schema");
        });

        builder.OwnsMany(b => b.Images, option =>
        {
            option.ToTable("Images", "product");
            option.Property(b => b.ImageName)
                .IsRequired()
                .HasMaxLength(100);
        });


        builder.OwnsMany(b => b.Specifications, option =>
        {
            option.ToTable("Specifications", "product");

            option.Property(b => b.Key)
                .IsRequired()
                .HasMaxLength(50);

            option.Property(b => b.Key)
                .IsRequired()
                .HasMaxLength(100);
        });
    }
}
