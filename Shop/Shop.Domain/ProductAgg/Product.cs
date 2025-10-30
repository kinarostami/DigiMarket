using Common.Domain;
using Common.Domain.Exceptions;
using Common.Domain.Utils;
using Common.Domain.ValueObjects;
using Shop.Domain.ProductAgg.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.ProductAgg;

public class Product : AggregateRoot
{
    public Product()
    {
        
    }
    public string Title { get; set; }
    public string ImageName { get; set; }
    public string Discription { get; set; }
    public long CatgeoryId { get; set; }
    public long SubCategoryId { get; set; }
    public long SecondarySubCategoryId { get; set; }
    public string Slug { get; set; }
    public SeoData SeoData { get; set;  }
    public List<ProductImage> Images { get; set; }
    public List<ProductSpecification> Specifications { get; set; }

    public Product(string title, string imageName, string discription, long catgeoryId, long subCategoryId, 
        long secondarySubCategoryId, string slug, SeoData seoData,IProductDomainSerivce serivce)
    {
        Title = title;
        Guard(title, slug, discription, serivce);
        ImageName = imageName;
        Discription = discription;
        CatgeoryId = catgeoryId;
        SubCategoryId = subCategoryId;
        SecondarySubCategoryId = secondarySubCategoryId;
        Slug = slug;
        SeoData = seoData;
        Images = new List<ProductImage>();
        Specifications = new List<ProductSpecification>();
    }
    
    public void Edit(string title, string imageName, string discription, long catgeoryId, long subCategoryId, 
        long secondarySubCategoryId, string slug, SeoData seoData,IProductDomainSerivce serivce)
    {
        Title = title;
        Guard(title, slug, discription, serivce);
        ImageName = imageName;
        Discription = discription;
        CatgeoryId = catgeoryId;
        SubCategoryId = subCategoryId;
        SecondarySubCategoryId = secondarySubCategoryId;
        Slug = slug;
        SeoData = seoData;
    }

    public void SetProductimage(string imageName)
    {
        NullOrEmptyDomainDataException.CheckString(imageName,nameof(imageName));
        ImageName = imageName;  
    }

    public void AddImage(ProductImage images)
    {
        images.ProductId = Id;
        Images.Add(images);
    }

    public string RemoveImage(long Id)
    {
        var oldImage = Images.FirstOrDefault(x => x.Id == Id);
        if (oldImage == null)
            throw new InvalidDomainDataException("عکس یافت نشد");

        Images.Remove(oldImage);
        return ImageName;
    }

    public void SetSpefication(List<ProductSpecification> spefication)
    {
        spefication.ForEach(x => x.ProductId = Id);
        Specifications = spefication;
    }

    private void Guard(string title, string slug, string description, IProductDomainSerivce domainService)
    {
        NullOrEmptyDomainDataException.CheckString(title, nameof(title));
        NullOrEmptyDomainDataException.CheckString(description, nameof(description));
        NullOrEmptyDomainDataException.CheckString(slug, nameof(slug));

        if (slug != Slug)
            if (domainService.IsSlugExist(slug.ToSlug()))
                throw new SlugIsDuplicateException();
    }
}
