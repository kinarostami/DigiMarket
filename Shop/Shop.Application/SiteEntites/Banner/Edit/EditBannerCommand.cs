using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Common.Application.SecurityUtil;
using Common.Application.Validation;
using Common.Application.Validation.FluentValidations;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Shop.Application._Utilities;
using Shop.Domain.SiteEntities;
using Shop.Domain.SiteEntities.Repository;

namespace Shop.Application.SiteEntities.Banner.Edit;

public class EditBannerCommand : IBaseCommand
{
    public long Id { get; set; }
    public string Link { get; set; }
    public IFormFile? ImageFile { get; set; }
    public BannerPosition Position { get; set; }
}
public class EditBannerCommandHandler : IBaseCommandHandler<EditBannerCommand>
{
     readonly IBannerRepository _bannerRepository;
     readonly IFileService _fileService;

    public EditBannerCommandHandler(IBannerRepository bannerRepository, IFileService fileService)
    {
        _bannerRepository = bannerRepository;
        _fileService = fileService;
    }

    public async Task<OperationResult> Handle(EditBannerCommand request, CancellationToken cancellationToken)
    {
        var banner = await _bannerRepository.GetTracking(request.Id);
        if (banner == null)
            return OperationResult.NotFound();
        var imageName = banner.ImageName;
        var oldImage = banner.ImageName;

        if (request.ImageFile.IsImage())
            imageName = await _fileService
                .SaveFileAndGenerateName(request.ImageFile, Directories.BannerImage);
        banner.Edit(request.Link,imageName,request.Position);

        DeleteOldImage(request.ImageFile,oldImage);
        await _bannerRepository.Save();
        return OperationResult.Success();
    }
     void DeleteOldImage(IFormFile? imageFile, string oldImage)
    {
        if (imageFile.IsImage())
            _fileService.DeleteFile(Directories.BannerImage, oldImage);
    }
}
public class EditBannerCommandValidator : AbstractValidator<EditBannerCommand>
{
    public EditBannerCommandValidator()
    {
        RuleFor(r => r.ImageFile)
            .NotNull().WithMessage(ValidationMessages.required("عکس"))
        .JustImageFile();

        RuleFor(r => r.Link)
            .NotNull()
            .NotEmpty().WithMessage(ValidationMessages.required("لینک"));
    }
}