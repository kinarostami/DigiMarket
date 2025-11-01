using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Common.Application.Validation;
using Common.Application.Validation.FluentValidations;
using Common.Domain.ValueObjects;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Shop.Application._Utilities;
using Shop.Domain.ProductAgg;
using Shop.Domain.ProductAgg.Repository;
using Shop.Domain.ProductAgg.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Products.Edit;

public class EditProductCommand : IBaseCommand
{
    public EditProductCommand(long procutId, string title, IFormFile? imageFile, string description, 
        long categoryId, long subCategoryId, long secondarySubCategoryId, string slug, SeoData seoData, 
        Dictionary<string, string> specifications)
    {
        ProcutId = procutId;
        Title = title;
        ImageFile = imageFile;
        Description = description;
        CategoryId = categoryId;
        SubCategoryId = subCategoryId;
        SecondarySubCategoryId = secondarySubCategoryId;
        Slug = slug;
        SeoData = seoData;
        Specifications = specifications;
    }

    public long ProcutId { get; set; }
    public string Title { get; set; }
    public IFormFile? ImageFile { get; set; }
    public string Description { get; set; }
    public long CategoryId { get; set; }
    public long SubCategoryId { get; set; }
    public long SecondarySubCategoryId { get; set; }
    public string Slug { get; set; }
    public SeoData SeoData { get; set; }
    public Dictionary<string, string> Specifications { get; set; }
}
public class EditProductCommandHandler : IBaseCommandHandler<EditProductCommand>
{
    private readonly IProductRepository _repository;
    private readonly IProductDomainSerivce _productDomainSerivce;
    private readonly IFileService _fileService;

    public EditProductCommandHandler(IProductRepository repository, IProductDomainSerivce productDomainSerivce, IFileService fileService)
    {
        _repository = repository;
        _productDomainSerivce = productDomainSerivce;
        _fileService = fileService;
    }

    public async Task<OperationResult> Handle(EditProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _repository.GetTracking(request.ProcutId);
        if (product == null)
            return OperationResult.NotFound();

        product.Edit(request.Title, request.Description, request.CategoryId, request.SubCategoryId,
            request.SecondarySubCategoryId, request.Slug, request.SeoData, _productDomainSerivce);

        var oldImage = product.ImageName;

        if (request.ImageFile != null)
        {
            var imageName = await _fileService.SaveFileAndGenerateName(request.ImageFile, Directories.ProductImages);
            product.SetProductimage(imageName);
        }

        var specifications = new List<ProductSpecification>();
        request.Specifications.ToList().ForEach(specification =>
        {
            specifications.Add(new ProductSpecification(specification.Key, specification.Value));
        });
        product.SetSpefication(specifications);
        await _repository.Save();
        RemoveOldImage(request.ImageFile, oldImage);
        return OperationResult.Success();
    }
    void RemoveOldImage(IFormFile? imageFile, string oldImageName)
    {
        if (imageFile != null)
        {
            _fileService.DeleteFile(Directories.ProductImages, oldImageName);
        }
    }
}
public class EditProductCommandValidator : AbstractValidator<EditProductCommand>
{
    public EditProductCommandValidator()
    {
        RuleFor(x => x.Title).NotEmpty()
        .WithMessage(ValidationMessages.required("عنوان"));

        RuleFor(x => x.Description).NotEmpty()
            .WithMessage(ValidationMessages.required("توضیحات"));

        RuleFor(x => x.Title).NotEmpty()
            .WithMessage(ValidationMessages.required("عنوان"));

        RuleFor(x => x.ImageFile)
            .JustImageFile();

        RuleFor(x => x.Slug).NotEmpty()
            .WithMessage(ValidationMessages.required("slug"));
    }
}