using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Common.Application.Validation;
using Common.Application.Validation.FluentValidations;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Shop.Application._Utilities;
using Shop.Domain.ProductAgg;
using Shop.Domain.ProductAgg.Repository;
using Shop.Domain.ProductAgg.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Products.AddImage;

public class AddImageCommand : IBaseCommand
{
    public AddImageCommand(long productId, IFormFile imageFile, int sequence)
    {
        ProductId = productId;
        ImageFile = imageFile;
        Sequence = sequence;
    }

    public long ProductId { get; set; }
    public IFormFile ImageFile { get; set; }
    public int Sequence { get; set; }
}
public class AddImageCommandHandler : IBaseCommandHandler<AddImageCommand>
{
    private readonly IProductRepository _productRepository;
    private readonly IFileService _fileService;

    public AddImageCommandHandler(IProductRepository productRepository, IFileService fileService)
    {
        _productRepository = productRepository;
        _fileService = fileService;
    }

    public async Task<OperationResult> Handle(AddImageCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetTracking(request.ProductId);
        if (product == null)
            return OperationResult.NotFound();

        var imageName = await _fileService
            .SaveFileAndGenerateName(request.ImageFile, Directories.ProductGalleryImage);

        var productImage = new ProductImage(imageName,request.Sequence);
        product.AddImage(productImage);
        await _productRepository.Save();
        return OperationResult.Success();
    }
}
public class AddImageCommandValidator : AbstractValidator<AddImageCommand>
{
    public AddImageCommandValidator()
    {
        RuleFor(x => x.ImageFile)
            .NotNull().WithMessage(ValidationMessages.required("عکس")).JustImageFile();

        RuleFor(x => x.Sequence)
            .GreaterThanOrEqualTo(0);
    }
}