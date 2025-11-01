using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Shop.Application._Utilities;
using Shop.Domain.ProductAgg.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Products.RemoveImage;

public record RemoveImageCommand(long ProductId,long ImageId) : IBaseCommand;

public class RemoveImageCommandHandler : IBaseCommandHandler<RemoveImageCommand>
{
    private readonly IProductRepository _productRepository;
    private readonly IFileService _fileService;

    public RemoveImageCommandHandler(IProductRepository productRepository, IFileService fileService)
    {
        _productRepository = productRepository;
        _fileService = fileService;
    }

    public async Task<OperationResult> Handle(RemoveImageCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetTracking(request.ProductId);
        if (product == null)
            return OperationResult.NotFound();

        var removeImage = product.RemoveImage(request.ImageId);
        await _productRepository.Save();
        _fileService.DeleteFile(Directories.ProductGalleryImage,removeImage);
        return OperationResult.Success();
    }
}

