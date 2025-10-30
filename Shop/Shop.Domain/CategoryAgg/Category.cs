using Common.Domain;
using Common.Domain.Exceptions;
using Common.Domain.Utils;
using Common.Domain.ValueObjects;
using Shop.Domain.CategoryAgg.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.CategoryAgg;

public class Category : AggregateRoot
{
    public Category()
    {
        Child = new List<Category>();
    }

    public Category(string slug, string title, SeoData seoData, ICategoryDomainService service)
    {
        Slug = slug?.ToSlug();
        Guard(title, slug, service);
        Title = title;
        SeoData = seoData;
        Child = new List<Category>();
    }

    public string Slug { get; set; }
    public string Title { get; set; }
    public SeoData SeoData { get; set; }
    public long? ParentId { get; set; }
    public List<Category> Child { get; set; }

    public void Edit(string slug, string title, SeoData seoData, long? parentId,ICategoryDomainService service)
    {
        Slug = slug?.ToSlug();
        Guard(title, slug, service);
        Title = title;
        SeoData = seoData;
        ParentId = parentId;
        Child = new List<Category>();
    }

    public void AddChild(string slug, string title, SeoData seoData, long? parentId, ICategoryDomainService service)
    {
        Child.Add(new Category(slug, title, seoData,service)
        {
            ParentId = Id
        });
    }

    public void Guard(string title, string slug, ICategoryDomainService service)
    {
        NullOrEmptyDomainDataException.CheckString(title,nameof(title));
        NullOrEmptyDomainDataException.CheckString(slug,nameof(slug));

        if (slug != Slug)
            if (service.IsSlugExist(slug.ToSlug()))
                throw new SlugIsDuplicateException();
    }
}
