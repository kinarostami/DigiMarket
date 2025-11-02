using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Shop.Application._Utilities;
using Shop.Domain.SiteEntities.Repository;

namespace Shop.Application.SiteEntities.Banner.Delete;

public class DeleteBannerCommandHandler : IBaseCommandHandler<DeleteBannerCommand>
{
     readonly IBannerRepository _bannerRepository;
     readonly IFileService _fileService;

    public DeleteBannerCommandHandler(IBannerRepository bannerRepository, IFileService fileService)
    {
        _bannerRepository = bannerRepository;
        _fileService = fileService;
    }

    public async Task<OperationResult> Handle(DeleteBannerCommand request, CancellationToken cancellationToken)
    {
        var banner = await _bannerRepository.GetTracking(request.Id);
        if (banner == null)
            return OperationResult.NotFound();

        _bannerRepository.Delete(banner);
        await _bannerRepository.Save();
        _fileService.DeleteFile(Directories.BannerImage, banner.ImageName);
        return OperationResult.Success();
    }
}