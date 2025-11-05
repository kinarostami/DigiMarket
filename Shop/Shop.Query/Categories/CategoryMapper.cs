using Shop.Domain.CategoryAgg;
using Shop.Query.Categories.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Query.Categories;

public static class CategoryMapper
{
    public static CategoryDto Map(this Category category)
    {
        if (category == null)
            return null;

        return new CategoryDto()
        {
            Id = category.Id,
            Title = category.Title,
            SeoData = category.SeoData,
            Slug = category.Slug,
            CreationDate = category.CreationDate,
            Childs = category.Child.MapChildren()
        };
    }

    public static List<CategoryDto> Map(this List<Category> categories)
    {
        var model = new List<CategoryDto>();

        //categories.ForEach(x =>
        //{
        //    model.Add(new CategoryDto()
        //    {
        //        Id = x.Id,
        //        Title = x.Title,
        //        SeoData = x.SeoData,
        //        Slug = x.Slug,
        //        CreationDate = x.CreationDate,
        //        Childs = x.Child.MapChildren()
        //    });
        //});

        foreach (var item in categories)
        {
            model.Add(new CategoryDto()
            {
                Id = item.Id,
                Title = item.Title,
                SeoData = item.SeoData,
                Slug = item.Slug,
                CreationDate = item.CreationDate,
                Childs = item.Child.MapChildren()
            });
        }
        return model;
    }

    public static List<ChildCategoryDto> MapChildren(this List<Category> children)
    {
        var model = new List<ChildCategoryDto>();
        foreach (var item in children)
        {
            model.Add(new ChildCategoryDto()
            {
                Id = item.Id,
                Title = item.Title,
                SeoData = item.SeoData,
                Slug = item.Slug,
                CreationDate = item.CreationDate,
                ParentId = (long)item.ParentId,
                Childs = item.Child.MapSecondaryChild()
            });
        }
        return model;
    }

    public static List<SecondaryChildCategoryDto> MapSecondaryChild(this List<Category> children)
    {
        var model = new List<SecondaryChildCategoryDto>();
        foreach (var item in children)
        {
            model.Add(new SecondaryChildCategoryDto()
            {
                Id = item.Id,
                Title = item.Title,
                SeoData = item.SeoData,
                Slug = item.Slug,
                CreationDate = item.CreationDate,
                ParentId = (long)item.ParentId,
            });
        }
        return model;
    }
}
