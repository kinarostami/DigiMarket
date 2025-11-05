using Microsoft.EntityFrameworkCore;
using Shop.Domain.ProductAgg;
using Shop.Infrastucture.Persistent.Ef;
using Shop.Query.Products.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Query.Products;

public static class ProductMapper
{
    public static ProductDto? Map(this Product product)
    {
        if (product == null)
            return null;

        return new ProductDto()
        {
            Id = product.Id,
            CreationDate = product.CreationDate,
            Title = product.Title,
            Slug = product.Slug,
            Description = product.Discription,
            SeoData = product.SeoData,
            ImageName = product.ImageName,
            Specifications = product.Specifications.Select(x => new ProductSpecificationDto()
            {
                Value = x.Value,
                Key = x.Key,
            }).ToList(),
            Images = product.Images.Select(x => new ProductImageDto()
            {
                Id = x.Id,
                ProductId = x.ProductId,
                CreationDate = x.CreationDate,
                ImageName = x.ImageName,
                Sequence = x.Sequence,
            }).ToList(),
            Category = new ProductCategoryDto()
            {
                Id = product.CatgeoryId
            },
            SubCategory = new ()
            {
                Id = product.SubCategoryId
            },
            SecondaryChildCategory = product.SecondarySubCategoryId != null ? new ()
            {
                Id = (long)product.SecondarySubCategoryId
            }:null
        };
    }

    public static ProductFilterData MapListData(this Product product)
    {
        return new ProductFilterData()
        {
            Id = product.Id,
            Slug = product.Slug,
            CreationDate = product.CreationDate,
            Title = product.Title,
            ImageName = product.ImageName
        };
    }

    public static async Task SetCategories(this ProductDto product, ShopContext context)
    {
        var categories = await context.Categories
            .Where(x => x.Id == product.Category.Id || x.Id == product.SubCategory.Id)
            .Select(x => new ProductCategoryDto()
            {
                Id= x.Id,
                SeoData = x.SeoData,
                Slug= x.Slug,
                ParentId = x.ParentId,
                Title = x.Title,
            }).ToListAsync();

        if (product.SecondaryChildCategory != null)
        {
            var secondaryCategory = await context.Categories
                .Where(x => x.Id == product.SecondaryChildCategory.Id)
                .Select(x => new ProductCategoryDto()
                {
                    Id = x.Id,
                    SeoData = x.SeoData,
                    Slug = x.Slug,
                    ParentId = x.ParentId,
                    Title = x.Title,
                }).FirstOrDefaultAsync();

            if (secondaryCategory != null)
                product.SecondaryChildCategory = secondaryCategory;
        }

        product.Category = categories.First(x => x.Id == product.Category.Id);

        product.SubCategory = categories.First(x => x.Id == product.SubCategory.Id);
    }
}
